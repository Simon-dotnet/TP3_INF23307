using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock")]
public class StockController : Controller
{
    private readonly StockService _stockService;

    public StockController(StockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("../ModuleStock/HomepageStock");
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