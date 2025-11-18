using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/supplier")]
public class SupplierController : Controller
{
    private SupplierService _supplierService;
    public SupplierController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = _supplierService.GetAllSuppliers();
        return View("../ModuleStock/AllSupplier", result.Data);
    }

    [HttpGet]
    [Route("add")]
    public IActionResult AddSupplierForm()
    {
        return View("../ModuleStock/AddSupplier", new Supplier());
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddSupplier([FromForm] Supplier supplier)
    {
        var result = _supplierService.AddSupplier(supplier);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return View("../ModuleStock/AddSupplier", new Supplier());
        }
        
        TempData["ErrorMessage"] = "Fournisseur ajouté avec succès";
        TempData["ErrorType"] = "success";
        return View("../ModuleStock/AddSupplier", new Supplier());
    }
}