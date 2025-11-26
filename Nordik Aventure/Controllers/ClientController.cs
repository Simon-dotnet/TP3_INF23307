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

    [HttpGet("all")]
    public IActionResult GetAllClients()
    {
        var results = _clientService.GetAllClients();
        return View("../ModuleClient/AllClient", results.Data);
    }

    [HttpGet("{id}")]
    public ActionResult GetSingleClient(int id)
    {
        var result = _clientService.GetClientById(id);
        if (!result.Success)
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

    [HttpGet("/edit/{id}")]
    public ActionResult EditClientForm(int id)
    {
        var result = _clientService.GetClientById(id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }

        return View("../ModuleClient/EditClient", result.Data);
    }

    [HttpPost("/edit")]
    public ActionResult EditClient([FromForm] Client newClient)
    {
        var clientResult = _clientService.GetClientById(newClient.Id);
        if (!clientResult.Success)
        {
            TempData["ErrorMessage"] = clientResult.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }
        
        clientResult.Data.Address = newClient.Address;
        clientResult.Data.Name = newClient.Name;
        clientResult.Data.Email = newClient.Email;
        clientResult.Data.Phone = newClient.Phone;
        clientResult.Data.Password = newClient.Password;
        clientResult.Data.Status = newClient.Status;
        clientResult.Data.Type = newClient.Type;
        clientResult.Data.SatisfactionLevel = newClient.SatisfactionLevel;
        var result = _clientService.UpdateClient(clientResult.Data);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = clientResult.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");

        }
        TempData["ErrorMessage"] = "Client modifié avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetSingleClient", new { id = clientResult.Data.Id });

    }
}