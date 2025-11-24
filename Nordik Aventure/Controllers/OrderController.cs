using AutoMapper;
using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("order")]
public class OrderController : Controller
{
    private readonly NordikAventureContext _context;
    private readonly OrderService _orderService;
    private readonly StockService _stockService;
    private readonly StockMovementController _stockMovementController;
    private readonly PurchaseService _purchaseService;
    private readonly TransactionService _transactionService;
    private readonly TaxesService _taxesService;
    private readonly IMapper _mapper;

    public OrderController(NordikAventureContext context, OrderService orderService,
        StockService stockService, StockMovementController stockMovementController, PurchaseService purchaseService,
        TransactionService transactionService, TaxesService taxesService, IMapper mapper)
    {
        _context = context;
        _orderService = orderService;
        _stockService = stockService;
        _stockMovementController = stockMovementController;
        _purchaseService = purchaseService;
        _taxesService = taxesService;
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpGet("MakeOrder")]
    public IActionResult MakeOrder()
    {
        var viewModel = new MakeOrderViewModel
        {
            AvailableProducts = _context.Products.Include(p => p.Supplier).ToList(),
            Taxes = _context.Taxes.FirstOrDefault()
        };

        return View("../ModuleFinance/MakeOrder", viewModel);
    }

    [HttpGet("OrderHistory")]
    public IActionResult OrderHistory()
    {
        var result = _orderService.GetAllOrders();

        if (!result.Success || result.Data == null)
            return View("../ModuleFinance/OrderHistory", new List<OrderHistoryViewModel>());

        var orders = result.Data
            .OrderByDescending(o => o.DateOfOrdering)
            .Take(50)
            .ToList();

        var orderHistory = orders.Select(order => new OrderHistoryViewModel
        {
            OrderId = order.OrderId,
            DateOfOrdering = order.DateOfOrdering,
            DateOfDelivery = order.DateOfDelivery,
            TotalPrice = order.TotalPrice,
            Items = order.OrderSupplierProducts.Select(osp => new OrderItemViewModel
            {
                ProductName = osp.Product?.Name ?? "Non disponible",
                SupplierName = osp.Supplier?.Name ?? "Non disponible",
                Quantity = osp.Quantity,
                TotalPrice = osp.TotalPrice
            }).ToList()
        }).ToList();

        return View("../ModuleFinance/OrderHistory", orderHistory);
    }

    [HttpPost("SubmitOrder")]
    public IActionResult SubmitOrder([FromForm] OrderCreateViewModel model)
    {
        if (model == null || model.Items == null || !model.Items.Any())
        {
            TempData["ErrorMessage"] = "Aucune ligne de commande.";
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }

        var resultTransaction = AddEnteringTransaction(model);
        if (!resultTransaction.Success)
        {
            TempData["ErrorMessage"] = resultTransaction.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }

        var resultPurchase = AddEnteringPurchase(model, resultTransaction.Data.TransactionId);
        if (!resultPurchase.Success)
        {
            TempData["ErrorMessage"] = resultTransaction.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }

        var result = _orderService.CreateOrder(model, resultPurchase.Data.PurchaseId);
        if (result.Success)
        {
            var resultSuccess = CheckProductInStock(result.Data);
            if (!resultSuccess.isError)
            {
                if (resultSuccess.hasNewObject)
                {
                    TempData["ErrorMessage"] =
                        "Commande créé. \n Vous avez ajouté un nouveau produit, veuillez allez changer ces valeurs dans le stock pour le rendre actif";
                }
                else
                {
                    TempData["ErrorMessage"] = "Commande créée.";
                }

                TempData["ErrorType"] = "success";
                AddEnteringMovementHistory(result.Data.OrderId);
                return RedirectToAction("Index", "Finance");
            }

            return View("../ModuleFinance/OrderHistory", new List<OrderHistoryViewModel>());
        }

        TempData["ErrorMessage"] = "Erreur: " + result.Message;
        TempData["ErrorType"] = "error";
        return RedirectToAction("MakeOrder");
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return RedirectToAction("Index", "Finance");
    }

    private (bool isError, bool hasNewObject) CheckProductInStock(Order listItems)
    {
        bool isError = false;
        bool hasNewObject = false;

        foreach (var item in listItems.OrderSupplierProducts)
        {
            var stockResponse = _stockService.GetProductInStockFromProductId(item.ProductId);

            if (stockResponse.Success)
            {
                var product = stockResponse.Data;
                product.LastRefill = listItems.DateOfDelivery;
                product.QuantityInStock += item.Quantity;

                if (!_stockService.UpdateProductStockFromForm(product).Success)
                    isError = true;
            }
            else
            {
                var newProduct = new ProductInStock
                {
                    ProductId = item.ProductId,
                    QuantityInStock = item.Quantity,
                    LastRefill = listItems.DateOfDelivery,
                    MinimalQuantity = 0,
                    Threshold = 0,
                    Status = "Inactif",
                    StockId = 1,
                    StorageLocation = "Pas encore établi"
                };

                if (!_stockService.AddProductToStock(newProduct).Success)
                    isError = true;
                else
                    hasNewObject = true;
            }
        }

        if (isError)
        {
            TempData["ErrorMessage"] = "Erreur lors de l'update/ajout de la quantité du produit en stock";
            TempData["ErrorType"] = "error";
            return (true, false);
        }

        return (false, hasNewObject);
    }

    private void AddEnteringMovementHistory(int orderId)
    {
        var result = _stockMovementController.CreateEnteringStockMovement(orderId);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
        }
    }

    private GenericResponse<Transaction> AddEnteringTransaction(OrderCreateViewModel model)
    {
        var taxValues = _taxesService.GetTaxes();
        var sumPrice = model.Items.Sum(oicm => oicm.TotalPrice);
        var totalPrice = sumPrice + sumPrice * (taxValues.Data.ValueTvq / 100) +
                         sumPrice * (taxValues.Data.ValueTps / 100);
        var newTransaction = new Transaction
        {
            Amount = sumPrice,
            Type = "purchase",
            Date = DateTime.Now,
            AmountTps = taxValues.Data.ValueTps,
            AmountTvq = taxValues.Data.ValueTvq,
            AmountTotal = totalPrice,
        };
        return _transactionService.AddEnteringTransaction(newTransaction);
    }

    private GenericResponse<Purchase> AddEnteringPurchase(OrderCreateViewModel model, int transactionId)
    {
        var newPurchase = new Purchase
        {
            TransactionId = transactionId,
            PurchaseDetails = _mapper.Map<List<PurchaseDetails>>(model.Items),
        };

        return _purchaseService.AddPurchase(newPurchase);
    }
}