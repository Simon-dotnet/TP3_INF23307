using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("finance")]
public class FinanceController : Controller
{
    private readonly TransactionService _transactionService;

    public FinanceController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [Route("")]
    public IActionResult Index()
    {
        var lastTransactions = _transactionService.GetLastTransactions(5);
        return View("../ModuleFinance/HomepageFinance", lastTransactions.Data ?? new List<Nordik_Aventure.Objects.Models.Finance.Transaction>());
    }

    [HttpGet("transactions")]
    public IActionResult TransactionHistory()
    {
        var all = _transactionService.GetAllTransactions();
        return View("../ModuleFinance/TransactionHistory", all.Data ?? new List<Nordik_Aventure.Objects.Models.Finance.Transaction>());
    }

    [HttpGet("receipt/{transactionId}")]
    public IActionResult RedirectToReceipt(int transactionId)
    {
        var all = _transactionService.GetAllTransactions();
        var transaction = all.Data?.FirstOrDefault(t => t.TransactionId == transactionId);

        if (transaction == null)
            return NotFound();

        if (transaction.Type == "purchase")
            return Redirect($"/finance/purchase/receipt/{transactionId}");

        if (transaction.Type == "sale")
            return Redirect($"/finance/sale/receipt/{transactionId}");

        return NotFound();
    }
}