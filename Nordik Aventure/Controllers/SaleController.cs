using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("finance/sale")]
public class SaleController : Controller
{
    private readonly SaleService _saleService;

    public SaleController(SaleService saleService)
    {
        _saleService = saleService;
    }
    
        
    [HttpGet("{id}")]
    public Sale GetSaleById(int id)
    {
        var result = _saleService.GetSaleById(id);
        if (!result.Success)
        {
            return null;
        }
        return result.Data;
    }
}