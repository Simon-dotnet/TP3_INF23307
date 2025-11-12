using Microsoft.AspNetCore.Mvc;
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
}