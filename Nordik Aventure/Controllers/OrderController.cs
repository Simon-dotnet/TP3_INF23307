using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("order")]
public class OrderController : Controller
{
    private readonly NordikAventureContext _context;
    private readonly OrderService _orderService;
    private readonly ProductService _productService;
    private readonly StockService _stockService;

    public OrderController(NordikAventureContext context, OrderService orderService, ProductService productService,
        StockService stockService)
    {
        _context = context;
        _orderService = orderService;
        _productService = productService;
        _stockService = stockService;
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

        var result = _orderService.CreateOrder(model);
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
}