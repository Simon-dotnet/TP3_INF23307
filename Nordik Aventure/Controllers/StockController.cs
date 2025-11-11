using Microsoft.AspNetCore.Mvc;

namespace Nordik_Aventure.Controllers;

[Route("stock")]
public class StockController : Controller
{
    public StockController()
    {
        
    }

    public IActionResult Index()
    {
        return View("../ModuleStock/HomepageStock");
    }
}