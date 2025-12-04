using Microsoft.AspNetCore.Mvc;

namespace Nordik_Aventure.Controllers;

public class UserController : Controller
{
    public UserController()
    {
        
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
