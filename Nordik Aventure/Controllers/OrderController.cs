using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;
using Nordik_Aventure.Repositories;

[Route("order")]
public class OrderController : Controller
{
    private readonly NordikAventureContext _context;
    private readonly OrderService _orderService;
    private readonly ProductService _productService;

    public OrderController(NordikAventureContext context, OrderService orderService, ProductService productService)
    {
        _context = context;
        _orderService = orderService;
        _productService = productService;
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
            TempData["ErrorMessage"] = "Commande créée.";
            TempData["ErrorType"] = "success";
            return RedirectToAction("Index", "Finance");
        }
        else
        {
            TempData["ErrorMessage"] = "Erreur: " + result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("MakeOrder");
        }
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return RedirectToAction("Index", "Finance");
    }
}