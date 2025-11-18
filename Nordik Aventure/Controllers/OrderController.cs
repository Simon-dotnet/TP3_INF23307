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
            AvailableProducts = _context.Products.Include(p => p.Supplier).ToList()
        };
        return View("../ModuleFinance/MakeOrder", viewModel);
    }
    
    [HttpGet("OrderHistory")]
    public async Task<IActionResult> OrderHistory()
    {
        var orders = await _context.Orders
            .Include(order => order.OrderSupplierProducts)
            .ThenInclude(orderSupplierProduct => orderSupplierProduct.Product)
            .Include(order => order.OrderSupplierProducts)
            .ThenInclude(orderSupplierProduct => orderSupplierProduct.Supplier)
            .OrderByDescending(order => order.DateOfOrdering)
            .Take(50)
            .ToListAsync();
        
        var orderHistory = orders.Select(order => new OrderHistoryViewModel
        {
            OrderId = order.OrderId,
            DateOfOrdering = order.DateOfOrdering,
            DateOfDelivery = order.DateOfDelivery,
            TotalPrice = order.TotalPrice,
            Items = order.OrderSupplierProducts.Select(orderSupplierProduct => new OrderItemViewModel
            {
                ProductName = orderSupplierProduct.Product?.Name ?? "Non disponible",
                SupplierName = orderSupplierProduct.Supplier?.Name ?? "Non disponible",
                Quantity = orderSupplierProduct.Quantity,
                TotalPrice = orderSupplierProduct.TotalPrice
            }).ToList()
        }).ToList();
        
        return View("../ModuleFinance/OrderHistory", orderHistory);
    }

    [HttpPost("SubmitOrder")]
    public IActionResult SubmitOrder([FromForm] OrderCreateViewModel model)
    {
        if (model == null || model.Items == null || !model.Items.Any())
        {
            TempData["Error"] = "Aucune ligne de commande.";
            return RedirectToAction("MakeOrder");
        }

        var result = _orderService.CreateOrder(model);
        if (result.Success)
        {
            TempData["Success"] = "Commande créée.";
            return RedirectToAction("Index", "Finance");
        }
        else
        {
            TempData["Error"] = "Erreur: " + result.Message;
            return RedirectToAction("MakeOrder");
        }
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return RedirectToAction("Index", "Finance");
    }
}