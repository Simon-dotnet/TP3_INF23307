using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class SaleReceiptRepository
{
    private readonly NordikAventureContext _context;

    public SaleReceiptRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<SaleReceipt> AddSaleReceipt(SaleReceipt receipt)
    {
        try
        {
            _context.SaleReceipts.Add(receipt);
            _context.SaveChanges();
            return new GenericResponse<SaleReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SaleReceipt>($"Erreur lors de la création du SaleReceipt: {ex}", 500);
        }
    }

    public GenericResponse<SaleReceipt> GetById(int saleReceiptId)
    {
        try
        {
            var receipt = _context.SaleReceipts
                .Where(r => r.SaleReceiptId == saleReceiptId)
                .Include(r => r.Sale)
                    .ThenInclude(s => s.SaleDetails)
                        .ThenInclude(sd => sd.ProductInStock)
                        .ThenInclude(pis => pis.Product)
                .Include(r => r.Payment)
                    .ThenInclude(pay => pay.Transaction)
                .Include(r => r.Sale)
                    .ThenInclude(s => s.Client)
                .FirstOrDefault();

            return new GenericResponse<SaleReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SaleReceipt>($"Erreur lors de la lecture du SaleReceipt: {ex}", 500);
        }
    }

    public GenericResponse<SaleReceipt> GetBySaleId(int saleId)
    {
        try
        {
            var receipt = _context.SaleReceipts
                .Where(r => r.SaleId == saleId)
                .Include(r => r.Sale)
                    .ThenInclude(s => s.SaleDetails)
                        .ThenInclude(sd => sd.ProductInStock)
                        .ThenInclude(pis => pis.Product)
                .Include(r => r.Payment)
                    .ThenInclude(pay => pay.Transaction)
                .Include(r => r.Sale)
                    .ThenInclude(s => s.Client)
                .FirstOrDefault();

            return new GenericResponse<SaleReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SaleReceipt>($"Erreur lors de la lecture du SaleReceipt par SaleId: {ex}", 500);
        }
    }
    
    public GenericResponse<SaleReceipt> GetByTransactionId(int transactionId)
    {
        try
        {
            var receipt = _context.SaleReceipts
                .Include(r => r.Sale)
                .ThenInclude(s => s.Client)
                .Include(r => r.Sale)
                .ThenInclude(s => s.SaleDetails)
                .ThenInclude(sd => sd.ProductInStock)
                .ThenInclude(pis => pis.Product)
                .Include(r => r.Payment)
                .ThenInclude(pay => pay.Transaction)
                .FirstOrDefault(r => r.Payment.TransactionId == transactionId);

            return new GenericResponse<SaleReceipt>(receipt);
        }
        catch (Exception ex)
        {
            return new GenericResponse<SaleReceipt>($"Erreur GetByTransactionId: {ex}", 500);
        }
    }
}
