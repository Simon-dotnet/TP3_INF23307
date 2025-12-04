using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class SupplierReceiptRepository
{
    private readonly NordikAventureContext _context;

    public SupplierReceiptRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<SupplierReceipt> AddSupplierReceipt(SupplierReceipt receipt)
    {
        try
        {
            _context.SupplierReceipts.Add(receipt);
            _context.SaveChanges();
            return new GenericResponse<SupplierReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SupplierReceipt>($"Erreur lors de la création du SupplierReceipt: {ex}", 500);
        }
    }

    public GenericResponse<SupplierReceipt> GetById(int supplierReceiptId)
    {
        try
        {
            var receipt = _context.SupplierReceipts
                .Where(r => r.SupplierReceiptId == supplierReceiptId)
                .Include(r => r.Purchase)
                    .ThenInclude(p => p.PurchaseDetails)
                        .ThenInclude(pd => pd.Product)
                            .ThenInclude(p => p.Supplier)
                .Include(r => r.Payment)
                    .ThenInclude(pay => pay.Transaction)
                .FirstOrDefault();

            return new GenericResponse<SupplierReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SupplierReceipt>($"Erreur lors de la lecture du SupplierReceipt: {ex}", 500);
        }
    }

    public GenericResponse<SupplierReceipt> GetByPurchaseId(int purchaseId)
    {
        try
        {
            var receipt = _context.SupplierReceipts
                .Where(r => r.PurchaseId == purchaseId)
                .Include(r => r.Purchase)
                    .ThenInclude(p => p.PurchaseDetails)
                        .ThenInclude(pd => pd.Product)
                            .ThenInclude(p => p.Supplier)
                .Include(r => r.Payment)
                    .ThenInclude(pay => pay.Transaction)
                .FirstOrDefault();

            return new GenericResponse<SupplierReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SupplierReceipt>($"Erreur lors de la lecture du SupplierReceipt par PurchaseId: {ex}", 500);
        }
    }
    
    public GenericResponse<SupplierReceipt> GetByTransactionId(int transactionId)
    {
        try
        {
            var receipt = _context.SupplierReceipts
                .Include(r => r.Purchase)
                .ThenInclude(p => p.PurchaseDetails)
                .ThenInclude(pd => pd.Product)
                .ThenInclude(p => p.Supplier)
                .Include(r => r.Payment)
                .ThenInclude(pay => pay.Transaction)
                .FirstOrDefault(r => r.Payment.TransactionId == transactionId);

            return new GenericResponse<SupplierReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SupplierReceipt>($"Erreur GetByTransactionId: {ex}", 500);
        }
    }

}
