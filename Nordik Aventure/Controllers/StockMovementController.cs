using GestBibli.Objects;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/movement")]
public class StockMovementController : Controller
{
    private readonly MovementHistoryService _movementHistoryService;
    private readonly UserService _userService;
    private readonly UserSession _userSession;
    private readonly OrderService _orderService;

    public StockMovementController(MovementHistoryService movementHistoryService, UserService userService,
        UserSession userSession, OrderService orderService)
    {
        _movementHistoryService = movementHistoryService;
        _userService = userService;
        _userSession = userSession;
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = _movementHistoryService.GetLast30MovementHistory();
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Stock");
        }

        return View("../ModuleStock/StockMovement", result.Data);
    }

    public GenericResponse<MovementHistory> CreateEnteringStockMovement(int id)
    {
        var userId = _userSession.UserId;
        var order = _orderService.GetOrderById(id);
        if (!order.Success)
        {
            return new GenericResponse<MovementHistory>("Order not found", 404);
        }
        
        var orderData = order.Data;
        var currentEmployee = _userService.GetEmployeeById(userId.Value);
        var motifBuilder =
            $"{currentEmployee.Data.Name} {currentEmployee.Data.Surname} a acheté(e) le {orderData.DateOfOrdering:dd/MM/yyyy}: " +
            $"{string.Join("", orderData.OrderSupplierProducts.Select(osp => $"\n • {osp.Quantity} - {osp.Product.Name}"))} " +
            $"\npour une quantité totale de {orderData.TotalPrice} $";

        var movementHistory = new MovementHistory()
        {
            Type = "purchase",
            Date = orderData.DateOfDelivery,
            Motif = motifBuilder,
            PurchaseId = orderData.PurchaseId,
            SaleId = null,
            EmployeeId = currentEmployee.Data.Id
        };
        var result = _movementHistoryService.AddEnteringMovementHistory(movementHistory);
        return result;
    }
}