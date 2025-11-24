using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nordik_Aventure.Controllers;

[Route("client")]
public class ClientController : Controller
{
    private readonly NordikAventureContext _context;

    public ClientController(NordikAventureContext context)
    {
        _context = context;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        return View("../ModuleClient/HomepageClient");
    }
}