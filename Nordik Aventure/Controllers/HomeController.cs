using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

public class HomeController : Controller
{
    UserService _userService;
    public HomeController(UserService userService)
    {
        _userService = userService;
    }

    [Route("/")]
    public IActionResult Login()
    {
        return View("Login");
    }

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
        
        HttpContext.Session.SetString("userId", result.Data.Id.ToString());
        return View("Index", result.Data);
    }

    [Route("/login")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("userId");
        HttpContext.Session.Clear();
        return View("Login");
    }
    
    [Route("homepage")]
    public IActionResult Index()
    {
        var id = HttpContext.Session.GetString("userId");
        var currentEmployee = _userService.GetEmployeeById(Convert.ToInt32(id));
        if (currentEmployee.Code == 404)
        {
            TempData["ErrorMessage"] = currentEmployee.Message;
            TempData["ErrorType"] = "error";
            return View("Login");
        }
        
        return View("Index", currentEmployee.Data);
    }
}