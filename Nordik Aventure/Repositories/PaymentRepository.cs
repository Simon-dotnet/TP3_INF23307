using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class PaymentRepository
{
    private readonly NordikAventureContext _context;

    public PaymentRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<Payment> AddPayment(Payment payment)
    {
        try
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return new GenericResponse<Payment>(payment);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Payment>($"Erreur lors de la création du paiement: {ex}", 500);
        }
    }

    public GenericResponse<Payment> GetByTransactionId(int transactionId)
    {
        try
        {
            var payment = _context.Payments.FirstOrDefault(p => p.TransactionId == transactionId);
            return new GenericResponse<Payment>(payment);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Payment>($"Erreur lors de la lecture du paiement: {ex}", 500);
        }
    }

    public GenericResponse<List<Payment>> GetAllPayments()
    {
        var list = _context.Payments
            .Include(p => p.Transaction)
            .OrderByDescending(p => p.Transaction.Date)
            .ToList();

        return new GenericResponse<List<Payment>>(list);
    }

    public GenericResponse<Payment> UpdatePaymentStatus(int paymentId, string newStatus, double? remaining)
    {
        var payment = _context.Payments.SingleOrDefault(p => p.PaymentId == paymentId);
        if (payment == null)
            return new GenericResponse<Payment>("Paiement introuvable", 404);

        payment.Status = newStatus;
        payment.RemainingBalance = remaining ?? 0;

        _context.SaveChanges();

        return new GenericResponse<Payment>(payment);
    }

    public GenericResponse<List<dynamic>> GetPaymentItems(int paymentId)
    {
        var payment = _context.Payments
            .Include(p => p.Transaction)
            .FirstOrDefault(p => p.PaymentId == paymentId);

        if (payment == null)
            return new GenericResponse<List<dynamic>>("Paiement introuvable", 404);

        var transaction = payment.Transaction;

        if (transaction.Type == "sale")
        {
            var sale = _context.Sales
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.ProductInStock)
                .ThenInclude(ps => ps.Product)
                .ThenInclude(pr => pr.Supplier)
                .FirstOrDefault(s => s.TransactionId == transaction.TransactionId);

            if (sale == null)
                return new GenericResponse<List<dynamic>>("Vente introuvable", 404);

            var items = sale.SaleDetails.Select(sd => new
            {
                Product = sd.ProductInStock?.Product?.Name ?? "N/A",
                Supplier = sd.ProductInStock?.Product?.Supplier?.Name ?? "N/A",
                Quantity = sd.Quantity,
                Total = sd.TotalPrice
            }).ToList<dynamic>();

            return new GenericResponse<List<dynamic>>(items);
        }

        if (transaction.Type == "purchase")
        {
            var purchase = _context.Purchases
                .Include(p => p.PurchaseDetails)
                .ThenInclude(pd => pd.Product)
                .ThenInclude(pr => pr.Supplier)
                .FirstOrDefault(p => p.TransactionId == transaction.TransactionId);

            if (purchase == null)
                return new GenericResponse<List<dynamic>>("Achat introuvable", 404);

            var items = purchase.PurchaseDetails.Select(pd => new
            {
                Product = pd.Product?.Name ?? "N/A",
                Supplier = pd.Product?.Supplier?.Name ?? "N/A",
                Quantity = pd.Quantity,
                Total = Math.Round(pd.Quantity * pd.Product?.PriceToBuy ?? 0, 2)
            }).ToList<dynamic>();

            return new GenericResponse<List<dynamic>>(items);
        }

        return new GenericResponse<List<dynamic>>("Type de transaction inconnu", 400);
    }
    
    public GenericResponse<Payment> GetById(int paymentId)
    {
        var payment = _context.Payments
            .Include(p => p.Transaction)
            .FirstOrDefault(p => p.PaymentId == paymentId);

        return payment == null
            ? new GenericResponse<Payment>("Paiement introuvable", 404)
            : new GenericResponse<Payment>(payment);
    }

    public GenericResponse<Payment> UpdatePayment(Payment payment)
    {
        try
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
            return new GenericResponse<Payment>(payment);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Payment>($"Erreur lors de la mise à jour du paiement: {ex.Message}", 500);
        }
    }
}