using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock")]
public class StockController : Controller
{
    private readonly StockService _stockService;
    private readonly TransactionService _transactionService;
    private readonly SaleService _saleService;

    public StockController(StockService stockService, TransactionService transactionService, SaleService saleService)
    {
        _stockService = stockService;
        _transactionService = transactionService;
        _saleService = saleService;
    }

    [HttpGet]
    // Le stock comprend tout les produits in stock
    // Ceci est la page d'accueil du module stock
    public IActionResult Index()
    {
        // Va chercher les produits dans le stock que leur quantité est plus petite que leur seuil
        var stockToRefill = _stockService.GetProductInStockToRefill();
        // Va chercher les profits de la semaine (Vente - Achat)
        var profit = _transactionService.GetProfitFromWeek();
        // Va chercher la liste des ventes faites durant la semaine
        var salesOfTheWeek = _saleService.GetSalesOfTheWeek();
        if (!stockToRefill.Success || !profit.Success || !salesOfTheWeek.Success)
        {
            TempData["ErrorMessage"] = "Probleme durant le chargement du tableau de bord";
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Home");
        }

        var dashBoardVM = new StockDashboard()
        {
            ProductInStockToRefill = stockToRefill.Data,
            ProfitOfTheWeek = profit.Data,
            SalesOfTheWeek = salesOfTheWeek.Data
        };
        
        return View("../ModuleStock/DashboardStock", dashBoardVM);
    }

    [HttpGet]
    [Route("stock")]
    // Avoir tout les produits dans le stock
    public IActionResult GetStock()
    {
        var result = _stockService.GetStock();
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Home");
        }

        return View("../ModuleStock/AllStockProduct", result.Data);
    }

    [HttpGet]
    [Route("stock/{id}")]
    // Avoir un produit spécifique dans le stock
    public IActionResult DisplayProductStock(int id)
    {
        var result = _stockService.GetProductInStock(id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Home");
        }

        return View("../ModuleStock/SingleStockProduct", result.Data);
    }

    [HttpGet]
    [Route("stock/update/{id}")]
    // Avoir le formulaire de modification d'un produit dans le stock
    public IActionResult UpdateProductStockForm(int id)
    {
        var result = _stockService.GetProductInStock(id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("GetStock", "Stock");
        }

        return View("../ModuleStock/ModifyStockProduct", result.Data);
    }

    [HttpPost]
    [Route("stock/update")]
    // Modifier un produit dans le stock
    public IActionResult UpdateProductStock([FromForm] ProductInStock productInStock)
    {
        // Regarde si le produit dans le stock existe
        var result = _stockService.GetProductInStock(productInStock.Id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = "Le produit en stock à modifier n'existe pas";
            TempData["ErrorType"] = "error";
            return RedirectToAction("GetStock", "Stock");
        }
        
        result.Data!.Status = productInStock.Status;
        result.Data.MinimalQuantity = productInStock.MinimalQuantity;
        result.Data.Threshold = productInStock.Threshold;
        result.Data.StorageLocation = productInStock.StorageLocation;
        result.Data.QuantityInStock = productInStock.QuantityInStock;
        result.Data.MinimalQuantity = productInStock.MinimalQuantity;
        
        // modifier
        var resultUpdate = _stockService.UpdateProductStockFromForm(result.Data);

        if (!resultUpdate.Success)
        {
            TempData["ErrorMessage"] = resultUpdate.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("GetStock", "Stock");
        }
        
        TempData["ErrorMessage"] = "Produit en stock modifié avec succès";
        TempData["ErrorType"] = "success";
        return RedirectToAction("DisplayProductStock", "Stock", new { id = productInStock.Id });
    }
}