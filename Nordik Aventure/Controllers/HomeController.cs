using Microsoft.AspNetCore.Mvc;

namespace Nordik_Aventure.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    [Route("/")]
    public IActionResult Login()
    {
        return View("Login");
    }
    
    [Route("homepage")]
    public IActionResult Index()
    {
        return View("Index");
    }
}