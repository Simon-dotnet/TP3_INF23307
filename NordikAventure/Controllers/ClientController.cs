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
    private readonly SaleService _saleService;

    public ClientController(ClientService clientService, SaleService saleService)
    {
        _clientService = clientService;
        _saleService = saleService;
    }

    
    // Page de base pour les client, la page est vide. Pas grave lol
    public ActionResult Index()
    {
        return View("../ModuleClient/HomepageClient");
    }

    // Permet d'aller chercher toute la liste des clients
    [HttpGet("all")]
    public IActionResult GetAllClients()
    {
        var results = _clientService.GetAllClients();
        return View("../ModuleClient/AllClient", results.Data);
    }

    // Permet d'avoir un seul client
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

    // Renvoie le formulaire de création d'un client
    [HttpGet("/create")]
    public ActionResult GetAddClientForm()
    {
        return View("../ModuleClient/CreateClientForm", new Client());
    }

    // Méthode qui créé un client
    [HttpPost("/create")]
    public ActionResult CreateClient([FromForm] Client client)
    {
        var result = _clientService.CreateClient(client);
        // si transaction a une erreur, renvoie un message d'erreur
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("GetAddClientForm");
        }

        // Envoie un message de succès
        TempData["ErrorMessage"] = "Client créé avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetSingleClient", new { id = result.Data.Id });
    }

    // Renvoie le formulaire de modification d'un client
    [HttpGet("/edit/{id}")]
    public ActionResult EditClientForm(int id)
    {
        var result = _clientService.GetClientById(id);
        // si transaction a une erreur, renvoie un message d'erreur
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }

        return View("../ModuleClient/EditClient", result.Data);
    }

    // Modifie un client
    [HttpPost("/edit")]
    public ActionResult EditClient([FromForm] Client newClient)
    {
        var clientResult = _clientService.GetClientById(newClient.Id);
        if (!clientResult.Success)
        {
            // Si le client que nous essayons de modifier n'existe pas, renvoie une erreur
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
            // si transaction a une erreur, renvoie un message d'erreur
            TempData["ErrorMessage"] = clientResult.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");

        }
        // Renvoie un message de succès
        TempData["ErrorMessage"] = "Client modifié avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetSingleClient", new { id = clientResult.Data.Id });
    }

    // Permet d'avoir toutes les ventes associées à un client en passant par sont Id
    [HttpGet("sale-historic/{clientId:int}")]
    public IActionResult GetAllSaleForClientById(int clientId)
    {
        var sales = _saleService.GetSalesByClient(clientId);
        var client = _clientService.GetClientById(clientId);
        if (!client.Success)
        {
            // Si le client existe pas, renvoie un message d'erreur
            TempData["ErrorMessage"] = client.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }
        ViewBag.ClientName = client.Data.Name;
        if (!sales.Success)
        {
            // Si un erreur survient lors du get des ventes, renvoie une erreur
            TempData["ErrorMessage"] = sales.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index");
        }
        return View("../ModuleClient/ClientSaleHistoric", sales.Data);
    }
}