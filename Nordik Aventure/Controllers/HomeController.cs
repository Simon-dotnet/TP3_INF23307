using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

public class HomeController : Controller
{
    private readonly UserService _userService;
    private readonly UserSession _userSession;
    public HomeController(UserService userService, UserSession userSession)
    {
        _userService = userService;
        _userSession = userSession;
    }

    [Route("/")]
    public IActionResult Login()
    {
        return View("Login");
    }

    // Connexion
    [HttpPost("/checklogin")]
    public IActionResult CheckLogin(string email, string password)
    {
        var result = _userService.GetEmployeeByEmailAndPassword(email, password);
        if (result.Code == 404)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return View("Login");
        }

        _userSession.UserId = result.Data.Id;
        return View("Index", result.Data);
    }

    // Déconnexion
    [Route("/login")]
    public IActionResult Logout()
    {
        // Enleve le UserId qui est dans la session
        _userSession.ClearSession();
        return View("Login");
    }
    
    // Accès a la homepage
    [Route("homepage")]
    public IActionResult Index()
    {
        var id = _userSession.UserId;
        if (id == null)
        {
            TempData["ErrorMessage"] = "L'id n'existe pas, vous êtes illégal";
            TempData["ErrorType"] = "error";
            return View("Login");
        }
        var currentEmployee = _userService.GetEmployeeById(id.Value);
        if (currentEmployee.Code == 404)
        {
            TempData["ErrorMessage"] = currentEmployee.Message;
            TempData["ErrorType"] = "error";
            return View("Login");
        }
        
        return View("Index", currentEmployee.Data);
    }
}