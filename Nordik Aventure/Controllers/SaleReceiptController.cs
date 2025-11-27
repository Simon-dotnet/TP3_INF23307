using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.ViewModels;
using Nordik_Aventure.Services;

[Route("finance/sale/receipt")]
public class SaleReceiptController : Controller
{
    private readonly SaleReceiptService _saleReceiptService;
    private readonly TaxesService _taxesService;

    public SaleReceiptController(
        SaleReceiptService saleReceiptService,
        TaxesService taxesService)
    {
        _saleReceiptService = saleReceiptService;
        _taxesService = taxesService;
    }

    [HttpGet("{transactionId}")]
    public IActionResult Index(int transactionId)
    {
        var receipt = _saleReceiptService.GetByTransactionId(transactionId);
        if (!receipt.Success || receipt.Data == null)
            return NotFound();

        ViewBag.FromFinance = TempData["FromFinance"] ?? false;

        var vm = new ClientSaleReceiptViewModel
        {
            SaleReceiptId = receipt.Data.SaleReceiptId,
            Sale = receipt.Data.Sale,
            Payment = receipt.Data.Payment,
            Taxes = _taxesService.GetTaxes().Data,
            Transaction = receipt.Data.Payment.Transaction
        };

        return View("../ModuleClient/SaleReceipt", vm);
    }
}