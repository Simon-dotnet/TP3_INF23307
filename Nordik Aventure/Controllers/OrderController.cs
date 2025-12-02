using AutoMapper;
using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.ViewModels;
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
    private readonly PaymentService _paymentService;
    private readonly SupplierReceiptService  _supplierReceiptService;
    private readonly IMapper _mapper;

    public OrderController(NordikAventureContext context, OrderService orderService,
        StockService stockService, StockMovementController stockMovementController, PurchaseService purchaseService,
        TransactionService transactionService, TaxesService taxesService, PaymentService paymentService, SupplierReceiptService supplierReceiptService, IMapper mapper)
    {
        _context = context;
        _orderService = orderService;
        _stockService = stockService;
        _stockMovementController = stockMovementController;
        _purchaseService = purchaseService;
        _taxesService = taxesService;
        _transactionService = transactionService;
        _paymentService = paymentService;
        _supplierReceiptService = supplierReceiptService;
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

        var history = orders.Select(order =>
        {
            var supplierName = order.OrderSupplierProducts
                .FirstOrDefault()?.Supplier?.Name ?? "N/A";

            return new OrderHistoryViewModel
            {
                OrderId = order.OrderId,
                DateOfOrdering = order.DateOfOrdering,
                DateOfDelivery = order.DateOfDelivery,
                TotalPrice = order.TotalPrice,
                SupplierName = supplierName,
                Status = order.Status ?? "réception",
                Items = order.OrderSupplierProducts.Select(osp => new OrderItemViewModel
                {
                    ProductName = osp.Product?.Name ?? "Non disponible",
                    SupplierName = osp.Supplier?.Name ?? "Non disponible",
                    Quantity = osp.Quantity,
                    TotalPrice = osp.TotalPrice
                }).ToList()
            };
        }).ToList();

        return View("../ModuleFinance/OrderHistory", history);
    }
    
    // Fait une commande
    [HttpPost("SubmitOrder")]
    public IActionResult SubmitOrder([FromForm] OrderCreateViewModel model)
    {
        if (model == null || model.Items == null || !model.Items.Any())
        {
            TempData["ErrorMessage"] = "Aucune ligne de commande.";
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }
        // Ajoute une transaction de type Entering
        var resultTransaction = AddEnteringTransaction(model);
        if (!resultTransaction.Success)
        {
            TempData["ErrorMessage"] = resultTransaction.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }
        var tx = resultTransaction.Data;
        var newPayment = new Payment
        {
            TransactionId = tx.TransactionId,
            Amount = tx.AmountTotal,
            Status = "en attente",
            Type = "purchase"
        };
        var paymentResult = _paymentService.AddPayment(newPayment);
        if (!paymentResult.Success)
        {
            TempData["ErrorMessage"] = paymentResult.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }
        // Ajoute un nouvel achat
        var resultPurchase = AddEnteringPurchase(model, resultTransaction.Data.TransactionId);
        if (!resultPurchase.Success)
        {
            TempData["ErrorMessage"] = resultTransaction.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }

        // Ajout d'une commande
        var result = _orderService.CreateOrder(model, resultPurchase.Data.PurchaseId);
        if (result.Success)
        {
            // Regarde si le produit existe deja en sotck
            var resultSuccess = CheckProductInStock(result.Data);
            if (!resultSuccess.isError)
            {
                var receipt = new SupplierReceipt
                {
                    PurchaseId = resultPurchase.Data.PurchaseId,
                    PaymentId = paymentResult.Data.PaymentId,
                    Status = "created"
                };

                // Ajout de la facture
                var receiptResult = _supplierReceiptService.AddSupplierReceipt(receipt);
                if (!receiptResult.Success)
                {
                    TempData["ErrorMessage"] = "Commande créée, mais erreur lors de la création du reçu: " + receiptResult.Message;
                    TempData["ErrorType"] = "warning";
                    return RedirectToAction("OrderHistory");
                }

                TempData["ErrorType"] = "success";
                // Ajout du mouvement dans le stock de type entering
                AddEnteringMovementHistory(result.Data.OrderId);
                
                return RedirectToAction("Receipt", new { id = receiptResult.Data.SupplierReceiptId });
            }

            return View("../ModuleFinance/OrderHistory", new List<OrderHistoryViewModel>());
        }

        TempData["ErrorMessage"] = "Erreur: " + result.Message;
        TempData["ErrorType"] = "error";
        return RedirectToAction("MakeOrder");
    }
    
    [HttpGet("Receipt/{id}")]
    public IActionResult Receipt(int id)
    {
        var receiptResult = _supplierReceiptService.GetById(id);
        if (!receiptResult.Success || receiptResult.Data == null)
        {
            TempData["ErrorMessage"] = receiptResult.Message ?? "Reçu introuvable.";
            TempData["ErrorType"] = "error";
            return RedirectToAction("OrderHistory");
        }
        
        var receipt = receiptResult.Data;
        var taxes = _taxesService.GetTaxes().Data;

        var vm = new SupplierReceiptViewModel
        {
            SupplierReceiptId = receipt.SupplierReceiptId,
            Purchase = receipt.Purchase,
            Payment = receipt.Payment,
            Transaction = receipt.Payment?.Transaction,
            Status = receipt.Status,
            Taxes = taxes
        };

        return View("../ModuleFinance/SupplierReceipt", vm);
    }
    
    [HttpGet("Manage")]
    public IActionResult Manage()
    {
        var result = _orderService.GetAllOrders();
        if (!result.Success || result.Data == null)
            return View("../ModuleFinance/ManageOrders", new Tuple<List<OrderManageViewModel>, List<OrderManageViewModel>>(
                new List<OrderManageViewModel>(), new List<OrderManageViewModel>()
            ));

        var active = result.Data
            .Where(o => o.Status != "payée/fermée")
            .OrderBy(o => o.DateOfDelivery)
            .Select(o => new OrderManageViewModel
            {
                OrderId = o.OrderId,
                SupplierName = string.Join(", ",
                    o.OrderSupplierProducts
                        .Select(osp => osp.Supplier?.Name)
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Distinct()
                ),
                Status = o.Status ?? "réception",
                DateOfOrdering = o.DateOfOrdering,
                DateOfDelivery = o.DateOfDelivery,
                TotalPrice = o.TotalPrice
            })
            .ToList();

        var completed = result.Data
            .Where(o => o.Status == "payée/fermée")
            .OrderByDescending(o => o.DateOfDelivery)
            .Select(o => new OrderManageViewModel
            {
                OrderId = o.OrderId,
                SupplierName = o.OrderSupplierProducts.FirstOrDefault()?.Supplier?.Name ?? "-",
                Status = o.Status ?? "réception",
                DateOfOrdering = o.DateOfOrdering,
                DateOfDelivery = o.DateOfDelivery,
                TotalPrice = o.TotalPrice
            })
            .ToList();

        return View("../ModuleFinance/ManageOrders", Tuple.Create(active, completed));
    }
    
    [HttpGet("GetItems/{orderId}")]
    public IActionResult GetItems(int orderId)
    {
        var result = _orderService.GetOrderItems(orderId);
        if (!result.Success) return NotFound();

        var items = result.Data.Select(i => new
        {
            product = i.Product?.Name ?? "N/A",
            supplier = i.Supplier?.Name ?? "N/A",
            quantity = i.Quantity,
            total = i.TotalPrice
        });

        return Json(items);
    }
    
    [HttpPost("Delete")]
    public IActionResult Delete(int orderId)
    {
        var result = _orderService.DeleteOrder(orderId);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
        }
        else
        {
            TempData["ErrorMessage"] = "Commande supprimée.";
            TempData["ErrorType"] = "success";
        }
    
        return RedirectToAction("Manage");
    }

    [HttpPost("UpdateStatus")]
    public IActionResult UpdateStatus(int orderId, string status)
    {
        var result = _orderService.ChangeOrderStatus(orderId, status);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
        }
        else
        {
            TempData["ErrorMessage"] = "Statut mis à jour.";
            TempData["ErrorType"] = "success";
        }

        return RedirectToAction("Manage");
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