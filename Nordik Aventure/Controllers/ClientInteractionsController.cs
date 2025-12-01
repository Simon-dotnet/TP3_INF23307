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

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{clientId:int}")]
    public IActionResult GetClientInteractionsByClientId(int clientId)
    {
        var clientInteractions = _clientInteractionsService.GetClientInterractionsByClient(clientId);
        var clientResult = _clientService.GetClientById(clientId);
        if (!clientResult.Success)
        {
            TempData["ErrorMessage"] = clientInteractions.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }

        ViewBag.ClientName = clientResult.Data.Name;
        ViewBag.ClientId = clientResult.Data.Id;
        if (!clientInteractions.Success)
        {
            TempData["ErrorMessage"] = clientInteractions.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }
        
        return View("../ModuleClient/ClientInterractionsList", clientInteractions.Data);
    }

    [HttpGet("getaddform/{clientId:int}")]
    public IActionResult GetAddClientInteractionForm(int clientId)
    {
        var clientResult = _clientService.GetClientById(clientId);
        var employeeId = _userSession.UserId;
        if (!clientResult.Success)
        {
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
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("Index", "Client");
        }
        
        TempData["ErrorMessage"] = "Interraction avec client ajouté avec succès!";
        TempData["ErrorType"] = "success";
        return RedirectToAction("GetClientInteractionsByClientId", "ClientInteractions", new { clientId = result.Data.ClientId });
    }
}