using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nordik_Aventure.Controllers;

[Route("finance")]
public class FinanceController : Controller
{
    private readonly NordikAventureContext _context;

    public FinanceController(NordikAventureContext context)
    {
        _context = context;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        return View("../ModuleFinance/HomepageFinance");
    }
}