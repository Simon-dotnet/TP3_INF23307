using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Services;

[Route("taxes")]
public class TaxesController : Controller
{
    private readonly NordikAventureContext _context;
    private readonly TaxesService _taxesService;

    public TaxesController(NordikAventureContext context, TaxesService taxesService)
    {
        _context = context;
        _taxesService = taxesService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var response = _taxesService.GetTaxes();
    
        if (!response.Success)
        {
            TempData["ErrorMessage"] = response.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("../ModuleFinance/HomepageFinance");
        }

        return View("../ModuleFinance/EditTaxes", response.Data);
    }
    
    [HttpPost("edit")]
    public IActionResult Edit(Taxes model)
    {
        if (!ModelState.IsValid)
            return View("../ModuleFinance/EditTaxes", model);

        var result = _taxesService.UpdateTaxes(model);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }

        TempData["ErrorType"] = "success";
        TempData["ErrorMessage"] = "Taxes modifiés avec succès";
        return RedirectToAction("Index");
        
    }
}