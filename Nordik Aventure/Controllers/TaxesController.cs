using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Controllers;

[Route("taxes")]
public class TaxesController : Controller
{
    private readonly TaxesService _taxesService;

    public TaxesController(TaxesService taxesService)
    {
        _taxesService = taxesService;
    }
    
    //Affiche la page pour modifier les valeurs TPS TVQ
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

    //Fonction utilisé pour mettre à jour les valeurs TPS TVQ
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