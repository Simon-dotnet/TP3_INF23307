using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;
[ApiController]
[Route("finance/purchase")]
public class PurchaseController : Controller
{
    private readonly PurchaseService _purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet("{id}")]
    public IActionResult GetPurchaseById(int id)
    {
        var result = _purchaseService.GetPurchaseById(id);
        if (!result.Success || result.Data == null)
        {
            return Json(new List<PurchaseProductHistoricModalModel>());
        }

        var listPurchaseDto = result.Data.PurchaseDetails.Select(pd => new PurchaseProductHistoricModalModel
        {
            TotalPrice = pd.Quantity * pd.Product.PriceToBuy, 
            NameProduct = pd.Product.Name,
            Sku = pd.Product.Sku,
            SupplierName = pd.Product.Supplier.Name,
            Quantity = pd.Quantity
        }).ToList();

        return Ok(listPurchaseDto);
    }
}