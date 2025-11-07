using Microsoft.AspNetCore.Mvc;

namespace Nordik_Aventure.Controllers;

public class UserController : Controller
{
    public UserController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LoginView()
    {
        return View();
    }

    public IActionResult Login()
    {
        return null;
    }
    
}
