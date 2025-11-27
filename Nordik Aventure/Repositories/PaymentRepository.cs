using GestBibli.Objects;
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
}