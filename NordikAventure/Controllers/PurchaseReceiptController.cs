using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.ViewModels;
using Nordik_Aventure.Services;

[Route("finance/purchase/receipt")]
public class PurchaseReceiptController : Controller
{
    private readonly SupplierReceiptService _supplierReceiptService;
    private readonly TaxesService _taxesService;

    public PurchaseReceiptController(
        SupplierReceiptService supplierReceiptService,
        TaxesService taxesService)
    {
        _supplierReceiptService = supplierReceiptService;
        _taxesService = taxesService;
    }
    
    //Page affiché pour les factures de commandes
    [HttpGet("{transactionId}")]
    public IActionResult Index(int transactionId)
    {
        var receipt = _supplierReceiptService.GetByTransactionId(transactionId);

        if (!receipt.Success || receipt.Data == null)
            return NotFound();

        var vm = new SupplierReceiptViewModel
        {
            SupplierReceiptId = receipt.Data.SupplierReceiptId,
            Purchase = receipt.Data.Purchase,
            Taxes = _taxesService.GetTaxes().Data,
            Payment = receipt.Data.Payment,
            Transaction = receipt.Data.Payment.Transaction
        };

        return View("../ModuleFinance/SupplierReceipt", vm);
    }
}