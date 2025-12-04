using GestBibli.Objects;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/movement")]
public class StockMovementController : Controller
{
    private readonly MovementHistoryService _movementHistoryService;
    private readonly UserService _userService;
    private readonly UserSession _userSession;
    private readonly OrderService _orderService;
    private readonly SaleService _saleService;

    public StockMovementController(MovementHistoryService movementHistoryService, UserService userService,
        UserSession userSession, OrderService orderService, SaleService saleService)
    {
        _movementHistoryService = movementHistoryService;
        _userService = userService;
        _userSession = userSession;
        _orderService = orderService;
        _saleService = saleService;
    }

    [HttpGet]
    // Page d'accueil des mouvements dans le stock
    public IActionResult Index()
    {
        // Va chercher les 30 dernier movement historique
        var result = _movementHistoryService.GetLast30MovementHistory();
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Stock");
        }

        return View("../ModuleStock/StockMovement", result.Data);
    }

    // Permet de créer un mouvement de stock de type entrant
    public GenericResponse<MovementHistory> CreateEnteringStockMovement(int id)
    {
        // Va chercher l'id de l'employé présentement connecté
        var userId = _userSession.UserId;

        if (userId == null)
            return new GenericResponse<MovementHistory>("Aucun utilisateur connecté.", 401);

        // Va chercher la commande selon son Id
        var order = _orderService.GetOrderById(id);
        if (!order.Success)
            return new GenericResponse<MovementHistory>("Order not found", 404);
    
        var orderData = order.Data;
        // Va chercher les infos de l'employé connecté
        var currentEmployee = _userService.GetEmployeeById(userId.Value);

        // Construit le motif de la transaction automatiquement selon les produits qui ont été acheté
        var motifBuilder =
            $"{currentEmployee.Data.Name} {currentEmployee.Data.Surname} a acheté(e) le {orderData.DateOfOrdering:dd/MM/yyyy}: " +
            $"{string.Join("", orderData.OrderSupplierProducts.Select(osp => $"\n • {osp.Quantity} - {osp.Product.Name}"))} " +
            $"\npour un coût totale de {orderData.TotalPrice} $ (avec taxes)";

        var movementHistory = new MovementHistory()
        {
            Type = "purchase",
            Date = orderData.DateOfDelivery,
            Motif = motifBuilder,
            PurchaseId = orderData.PurchaseId,
            SaleId = null,
            EmployeeId = currentEmployee.Data.Id
        };

        return _movementHistoryService.AddEnteringMovementHistory(movementHistory);
    }
    
    // Même chose que plus haut, mais pour les mouvements sortants (ventes)
    public GenericResponse<MovementHistory> CreateLeavingStockMovement(int id)
    {
        var userId = _userSession.UserId;

        if (userId == null)
            return new GenericResponse<MovementHistory>("Aucun utilisateur connecté.", 401);

        var sale = _saleService.GetSaleById(id);
        if (!sale.Success)
            return new GenericResponse<MovementHistory>("Sale not found", 404);

        var saleData = sale.Data;
        var currentEmployee = _userService.GetEmployeeById(userId.Value);

        var motifBuilder =
            $"{currentEmployee.Data.Name} {currentEmployee.Data.Surname} a vendu(e) le {saleData.DateOfSale:dd/MM/yyyy}: " +
            $"{string.Join("", saleData.SaleDetails.Select(osp => $"\n • {osp.Quantity} - {osp.ProductInStock.Product.Name}"))} " +
            $"\npour un coût de {saleData.TotalPrice} $ (avec taxes)";

        var movementHistory = new MovementHistory()
        {
            Type = "sale",
            Date = DateTime.Now,
            Motif = motifBuilder,
            PurchaseId = null,
            SaleId = saleData.Id,
            EmployeeId = currentEmployee.Data.Id
        };

        return _movementHistoryService.AddLeavingMovementHistory(movementHistory);
    }
}