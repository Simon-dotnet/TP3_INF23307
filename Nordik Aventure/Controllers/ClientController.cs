using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.User;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("client")]
public class ClientController : Controller
{
    private readonly ClientService _clientService;
    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    public ActionResult Index()
    {
        return View("../ModuleClient/HomepageClient");
    }

    [HttpGet("{id}")]
    public ActionResult GetSingleClient(int id)
    {
        var result = _clientService.GetClientById(id);
        if (result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }
        return View("../ModuleClient/SingleClient", result.Data);
    }

    [HttpGet("/create")]
    public ActionResult GetAddClientForm()
    {
        return View("../ModuleClient/CreateClientForm", new Client());
    }

    [HttpPost("/create")]
    public ActionResult CreateClient([FromForm] Client client)
    {
        var result = _clientService.CreateClient(client);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("GetAddClientForm");
        }
        
        TempData["ErrorMessage"] = "Client créé avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetSingleClient", new { id = result.Data.Id });
    }
}