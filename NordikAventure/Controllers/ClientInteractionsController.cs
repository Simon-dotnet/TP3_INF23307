using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.ViewModels;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[ApiController]
[Route("client/interactions")]
public class ClientInteractionsController : Controller
{
    private readonly ClientInteractionsService _clientInteractionsService;
    private readonly ClientService _clientService;
    private readonly UserSession _userSession;

    public ClientInteractionsController(ClientInteractionsService clientInteractionsService, ClientService clientService, UserSession userSession)
    {
        _clientInteractionsService = clientInteractionsService;
        _clientService = clientService;
        _userSession = userSession;
    }

    // Méthode inutile
    public IActionResult Index()
    {
        return View();
    }

    // Permet d'Avoir les interactions client avec l'id du client
    [HttpGet("{clientId:int}")]
    public IActionResult GetClientInteractionsByClientId(int clientId)
    {
        var clientInteractions = _clientInteractionsService.GetClientInterractionsByClient(clientId);
        var clientResult = _clientService.GetClientById(clientId);
        // Si le client n'existe pas, renvoie un message d'erreur
        if (!clientResult.Success)
        {
            TempData["ErrorMessage"] = clientInteractions.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }

        // Passe en meta data le nom du client et l'id du client
        ViewBag.ClientName = clientResult.Data.Name;
        ViewBag.ClientId = clientResult.Data.Id;
        
        if (!clientInteractions.Success)
        {
            // Si un erreur survient lors du get des interactions clients, envoie un message d'erreur
            TempData["ErrorMessage"] = clientInteractions.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }
        
        return View("../ModuleClient/ClientInterractionsList", clientInteractions.Data);
    }

    // Permet d'avoir le formulaire d'ajout d'interaction client
    [HttpGet("getaddform/{clientId:int}")]
    public IActionResult GetAddClientInteractionForm(int clientId)
    {
        var clientResult = _clientService.GetClientById(clientId);
        var employeeId = _userSession.UserId;
        if (!clientResult.Success)
        {
            // Renvoie message erreur si client existe pas
            TempData["ErrorMessage"] = clientResult.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }

        var clientInteractionModel = new ClientInteractionViewModal()
        {
            ClientId = clientId,
            ClientName = clientResult.Data.Name,
            EmployeeId = employeeId.Value,
            
        };
        return View("../ModuleClient/AddClientInteractions", clientInteractionModel);
    }

    // Permet d'ajouter une interaction client
    [HttpPost("add")]
    public IActionResult AddClientInteractionFromForm([FromForm] ClientInteractionViewModal clientInterractionModal)
    {
        var clientInteraction = new ClientInterraction()
        {
            ClientId = clientInterractionModal.ClientId,
            Description = clientInterractionModal.Description,
            Date = clientInterractionModal.Date,
            EmployeeId = clientInterractionModal.EmployeeId,
            Type = clientInterractionModal.Type,
        };
        
        var result = _clientInteractionsService.AddClientInteraction(clientInteraction);
        if (!result.Success)
        {
            // Renvoie message erreur si l'ajout a pas fonctionné
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }
        
        // Renvoie message de succès
        TempData["ErrorMessage"] = "Interraction avec client ajouté avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetClientInteractionsByClientId", "ClientInteractions", new { clientId = result.Data.ClientId });
    }
}