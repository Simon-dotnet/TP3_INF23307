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
    public IActionResult Index()
    {
        var stockToRefill = _stockService.GetProductInStockToRefill();
        var profit = _transactionService.GetProfitFromWeek();
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
    public IActionResult UpdateProductStock([FromForm] ProductInStock productInStock)
    {
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