using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Services;
using Nordik_Aventure.Objects.ViewModels;

namespace Nordik_Aventure.Controllers;

[Route("payment")]
public class PaymentController : Controller
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet("manage")]
    public IActionResult Manage()
    {
        var result = _paymentService.GetAllPayments();

        if (!result.Success || result.Data == null)
            return View("../ModuleFinance/ManagePayments", new List<PaymentManageViewModel>());

        var vm = result.Data.Select(p => new PaymentManageViewModel
        {
            PaymentId = p.PaymentId,
            TransactionId = p.TransactionId,
            Type = p.Type,
            Status = p.Status,
            AmountTotal = p.Transaction.AmountTotal,
            Date = p.Transaction.Date,
            RemainingBalance = p.RemainingBalance
        }).ToList();

        return View("../ModuleFinance/ManagePayments", vm);
    }
    
    [HttpPost("update-status")]
    public IActionResult UpdateStatus(int paymentId, string status, double? remaining)
    {
        var result = _paymentService.ChangeStatus(paymentId, status, remaining);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
        }
        else
        {
            TempData["ErrorMessage"] = "Statut mis à jour.";
            TempData["ErrorType"] = "success";
        }

        return RedirectToAction("Manage");
    }
    
    [HttpGet("items/{paymentId}")]
    public IActionResult GetItems(int paymentId)
    {
        var result = _paymentService.GetPaymentItems(paymentId);

        if (!result.Success || result.Data == null)
            return NotFound();

        return Json(result.Data);
    }
    
}