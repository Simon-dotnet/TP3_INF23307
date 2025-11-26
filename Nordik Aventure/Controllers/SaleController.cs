using AutoMapper;
using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
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
    private readonly IMapper _mapper;

    public SaleController(
        NordikAventureContext context,
        StockService stockService,
        StockMovementController stockMovementController,
        SaleService saleService,
        TransactionService transactionService,
        TaxesService taxesService,
        IMapper mapper)
    {
        _context = context;
        _stockService = stockService;
        _stockMovementController = stockMovementController;
        _saleService = saleService;
        _transactionService = transactionService;
        _taxesService = taxesService;
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
    public IActionResult SubmitSale([FromForm] SaleCreateViewModel model)
    {
        if (model == null || model.Items == null || !model.Items.Any())
        {
            SetError("Aucune ligne de commande.");
            return RedirectToAction("MakeSale");
        }

        var isError = CheckProductInStock(model);
        if (isError)
        {
            return RedirectToAction("MakeSale");
        }

        var resultTransaction = AddLeavingTransaction(model);
        if (!resultTransaction.Success)
        {
            SetError(resultTransaction.Message);
            return RedirectToAction("MakeSale");
        }

        var resultSale = AddLeavingSale(model, resultTransaction.Data.TransactionId);
        if (!resultSale.Success)
        {
            SetError(resultSale.Message);
            return RedirectToAction("MakeSale");
        }

        var sale = resultSale.Data;

        AddLeavingMovementHistory(sale.Id);

        TempData["ErrorMessage"] = "Commande faites avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("Index", "Client");
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return RedirectToAction("Index", "Client");
    }

    [HttpGet("{id}")]
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
        }).ToList();
        return Ok(listSaleDto);
    }

    private bool CheckProductInStock(SaleCreateViewModel sale)
    {
        var hasError = false;
        foreach (var item in sale.Items)
        {
            var stockResponse = _stockService.GetProductInStock(item.ProductStockId);
            if (!stockResponse.Success)
            {
                SetError($"Le produit ID {item.ProductStockId} n'existe pas en stock.");
                hasError = true;
                break;
            }

            var stockItem = stockResponse.Data;

            if (item.Quantity > stockItem.QuantityInStock)
            {
                SetError(
                    $"Impossible de vendre {item.Quantity} unités. Stock disponible: {stockItem.QuantityInStock}.");
                hasError = true;
                break;
            }

            stockItem.QuantityInStock -= item.Quantity;

            var resultSave = _stockService.UpdateProductStockFromForm(stockItem);
            if (!resultSave.Success)
            {
                SetError(resultSave.Message);
                hasError = true;
                break;
            }
        }

        if (hasError)
        {
            return true;
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