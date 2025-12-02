using AutoMapper;
using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.Models.User;
using Nordik_Aventure.Objects.ViewModels;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("finance/sale")]
public class SaleController : Controller
{
    private readonly NordikAventureContext _context;
    private readonly StockService _stockService;
    private readonly StockMovementController _stockMovementController;
    private readonly SaleService _saleService;
    private readonly TransactionService _transactionService;
    private readonly TaxesService _taxesService;
    private readonly PaymentService _paymentService;
    private readonly SaleReceiptService _saleReceiptService;
    private readonly ClientInteractionsService _clientInterractionService;
    private readonly UserSession _userSession;
    private readonly ClientService _clientService;
    private readonly IMapper _mapper;

    public SaleController(
        NordikAventureContext context,
        StockService stockService,
        StockMovementController stockMovementController,
        SaleService saleService,
        TransactionService transactionService,
        TaxesService taxesService,
        PaymentService paymentService,
        SaleReceiptService saleReceiptService,
        ClientInteractionsService clientInterractionService,
        UserSession userSession,
        ClientService clientService,
        IMapper mapper)
    {
        _context = context;
        _stockService = stockService;
        _stockMovementController = stockMovementController;
        _saleService = saleService;
        _transactionService = transactionService;
        _taxesService = taxesService;
        _paymentService = paymentService;
        _saleReceiptService = saleReceiptService;
        _clientInterractionService = clientInterractionService;
        _userSession = userSession;
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpGet("MakeSale")]
    public IActionResult MakeSale()
    {
        var taxes = _taxesService.GetTaxes().Data;

        var viewModel = new MakeSaleViewModel
        {
            AvailableProducts = _context.ProductInStock.Include(ps => ps.Product).ThenInclude(p => p.Supplier)
                .Where(ps => ps.Status == "Actif").ToList(),
            AvailableClients = _context.Client.Where(c => c.Status == "Actif").ToList(),
            Taxes = taxes
        };

        return View("../ModuleClient/MakeSale", viewModel);
    }

    [HttpPost("SubmitSale")]
    // Soumettre une vente (client qui achète un de nos produits)
    public IActionResult SubmitSale([FromForm] SaleCreateViewModel model)
    {
        if (model == null || model.Items == null || !model.Items.Any())
        {
            SetError("Aucune ligne de commande.");
            return RedirectToAction("MakeSale");
        }

        var client = _context.Client.FirstOrDefault(c => c.Id == model.ClientId);
        bool isFirstSale = !_saleService.ClientHasSales(model.ClientId);

        // Regarde si nous avons le produit en inventaire
        var isError = CheckProductInStock(model);
        if (isError)
            return RedirectToAction("MakeSale");

        // Création d'une transaction de type "Sortante"
        var resultTransaction = AddLeavingTransaction(model);
        if (!resultTransaction.Success)
        {
            SetError(resultTransaction.Message);
            return RedirectToAction("MakeSale");
        }

        // Création de la vente
        var resultSale = AddLeavingSale(model, resultTransaction.Data.TransactionId);
        if (!resultSale.Success)
        {
            SetError(resultSale.Message);
            return RedirectToAction("MakeSale");
        }

        var sale = resultSale.Data;

        var payResult = AddSalePayment(resultTransaction.Data.TransactionId, sale.TotalPrice);
        if (!payResult.Success)
        {
            SetError(payResult.Message);
            return RedirectToAction("MakeSale");
        }

        var receipt = new SaleReceipt
        {
            SaleId = sale.Id,
            PaymentId = payResult.Data.PaymentId,
            Status = "completed"
        };

        var receiptResult = _saleReceiptService.AddSaleReceipt(receipt);
        if (!receiptResult.Success)
        {
            SetError("Erreur lors de la création du reçu.");
            return RedirectToAction("Index", "Client");
        }

        // Création du mouvement dans le stock de type sortante
        AddLeavingMovementHistory(sale.Id);

        // Ajout de l'interaction avec le client quand il achete quelque chose
        var clientInteractionResult = AddClientInteraction(model);
        if (!clientInteractionResult.Success)
        {
            return RedirectToAction("Index", "Client");
        }

        TempData["ErrorMessage"] = "Commande faite avec succès!";
        TempData["ErrorType"] = "success";

        if (isFirstSale && client != null)
        {
            TempData["WelcomeClient"] = "true";
            TempData["WelcomeClientName"] = client.Name;
        }

        TempData["FromFinance"] = false;

        return RedirectToAction("Index", "SaleReceipt", new { transactionId = resultTransaction.Data.TransactionId });
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return RedirectToAction("Index", "Client");
    }

    [HttpGet("details/{id}")]
    // Avoir une vente par son Id
    public IActionResult GetSaleById(int id)
    {
        var result = _saleService.GetSaleById(id);
        if (!result.Success)
        {
            return Json(new List<SaleProductHistoricModalModel>());
        }

        var listSaleDto = result.Data.SaleDetails.Select(sd => new SaleProductHistoricModalModel
        {
            TotalPrice = sd.Quantity * sd.ProductInStock.Product.PriceToSell,
            NameProduct = sd.ProductInStock.Product.Name,
            Sku = sd.ProductInStock.Product.Sku,
            ClientName = sd.Sale.Client.Name,
            Quantity = sd.Quantity,
            UnitPrice = sd.ProductInStock.Product.PriceToSell,
        }).ToList();
        return Ok(listSaleDto);
    }

    private bool CheckProductInStock(SaleCreateViewModel sale)
    {
        foreach (var item in sale.Items)
        {
            var stockResponse = _stockService.GetProductInStock(item.ProductStockId);
            if (!stockResponse.Success)
            {
                SetError($"Le produit ID {item.ProductStockId} n'existe pas en stock.");
                return true;
            }

            var stockItem = stockResponse.Data;

            if (item.Quantity > stockItem.QuantityInStock)
            {
                SetError(
                    $"Impossible de vendre {item.Quantity} unités. Stock disponible: {stockItem.QuantityInStock}.");
                return true;
            }

            stockItem.QuantityInStock -= item.Quantity;
            var resultSave = _stockService.UpdateProductStockFromForm(stockItem);
            if (!resultSave.Success)
            {
                SetError(resultSave.Message);
                return true;
            }
        }

        return false;
    }

    private void AddLeavingMovementHistory(int saleId)
    {
        var result = _stockMovementController.CreateLeavingStockMovement(saleId);
        if (!result.Success)
        {
            SetError(result.Message);
        }
    }

    private GenericResponse<Transaction> AddLeavingTransaction(SaleCreateViewModel model)
    {
        var taxValues = _taxesService.GetTaxes().Data;
        var sumPrice = model.Items.Sum(i => i.TotalPrice);

        var tps = sumPrice * (taxValues.ValueTps / 100);
        var tvq = sumPrice * (taxValues.ValueTvq / 100);

        var transaction = new Transaction
        {
            Amount = sumPrice,
            Type = "sale",
            Date = DateTime.Now,
            AmountTps = tps,
            AmountTvq = tvq,
            AmountTotal = sumPrice + tps + tvq
        };

        return _transactionService.AddLeavingTransaction(transaction);
    }

    private GenericResponse<Sale> AddLeavingSale(SaleCreateViewModel model, int transactionId)
    {
        var totalSale = model.Items.Sum(i => i.TotalPrice);
        var tvq = totalSale * (_taxesService.GetTaxes().Data.ValueTvq / 100);
        var tps = totalSale * (_taxesService.GetTaxes().Data.ValueTps / 100);
        var totalPriceWithTaxes = totalSale + tvq + tps;

        var sale = new Sale
        {
            ClientId = model.ClientId,
            TransactionId = transactionId,
            SaleDetails = _mapper.Map<List<SaleDetails>>(model.Items),
            DateOfSale = DateTime.Now,
            TotalPrice = totalPriceWithTaxes,
        };

        return _saleService.AddSale(sale);
    }
    
    private GenericResponse<ClientInterraction> AddClientInteraction(SaleCreateViewModel model)
    {
        // Va chercher les infos du client (pour avoir son nom)
        var client = _clientService.GetClientById(model.ClientId);
        
        var newClientInteraction = new ClientInterraction()
        {
            ClientId = model.ClientId,
            Date = DateTime.Now,
            EmployeeId = _userSession.UserId.Value,
            Type = "Vente",
            Description = $"<p>{client.Data.Name} a acheté pour:</p>"
        };

        // Création dynamique de la liste des produits qu'on affiche dans la modal des interaction clients dans la description
        var itemsHtml = "<ul>";
        foreach (var item in model.Items)
        {
            itemsHtml += $"<li>{item.Quantity} x {_stockService.GetProductInStock(item.ProductStockId).Data.Product.Name} à {item.UnitPrice:C}</li>";
        }
        itemsHtml += "</ul>" +
                     $"<p>pour un total de {model.Items.Sum(i => i.TotalPrice):F2} $</p>";

        newClientInteraction.Description += itemsHtml;
        
        // Sauvegarder l'interaction client
        var result = _clientInterractionService.AddClientInteraction(newClientInteraction);
        if (!result.Success)
        {
            SetError(result.Message);
            return new GenericResponse<ClientInterraction>(result.Message, 404);
        }

        return new GenericResponse<ClientInterraction>(newClientInteraction);
    }
    
    private GenericResponse<Payment> AddSalePayment(int transactionId, double totalAmount)
    {
        var payment = new Payment
        {
            TransactionId = transactionId,
            Amount = totalAmount,
            Status = "pending",
            Type = "sale"
        };

        return _paymentService.AddPayment(payment);
    }
    

    private void SetError(string message)
    {
        TempData["ErrorMessage"] = message;
        TempData["ErrorType"] = "error";
    }

    private void SetSuccess(string message)
    {
        TempData["ErrorMessage"] = message;
        TempData["ErrorType"] = "success";
    }
}