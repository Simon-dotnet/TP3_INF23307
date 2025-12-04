using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;

    public PaymentService(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public GenericResponse<Payment> AddPayment(Payment payment)
    {
        return _paymentRepository.AddPayment(payment);
    }

    public GenericResponse<Payment> GetByTransactionId(int transactionId)
    {
        return _paymentRepository.GetByTransactionId(transactionId);
    }

    public GenericResponse<List<Payment>> GetAllPayments()
    {
        return _paymentRepository.GetAllPayments();
    }

    public GenericResponse<Payment> ChangeStatus(int paymentId, string status, double? amountPaidNow)
    {
        var paymentResponse = _paymentRepository.GetById(paymentId);

        if (!paymentResponse.Success || paymentResponse.Data == null)
            return new GenericResponse<Payment>("Paiement introuvable", 404);

        var payment = paymentResponse.Data;

        double total = Math.Round(payment.Transaction.AmountTotal, 2);
        double alreadyPaid = Math.Round(payment.RemainingBalance ?? 0, 2);
        double added = Math.Round(amountPaidNow ?? 0, 2);
        
        if (status == "partielle" && alreadyPaid + added > total)
        {
            double maxPossible = Math.Round(total - alreadyPaid, 2);
            return new GenericResponse<Payment>($"Le montant dépasse le solde restant. Maximum possible: {maxPossible:N2}$", 400);
        }
        
        if (status == "partielle")
        {
            double newPaid = alreadyPaid + added;

            if (newPaid >= total)
            {
                payment.Status = "payée";
                payment.RemainingBalance = total;
            }
            else
            {
                payment.Status = "partielle";
                payment.RemainingBalance = newPaid;
            }
        }
        else if (status == "en attente")
        {
            payment.Status = "en attente";
            payment.RemainingBalance = alreadyPaid;
        }
        else if (status == "payée")
        {
            payment.Status = "payée";
            payment.RemainingBalance = total;
        }
        else
        {
            return new GenericResponse<Payment>("Statut invalide", 400);
        }

        return _paymentRepository.UpdatePayment(payment);
    }

    public GenericResponse<List<dynamic>> GetPaymentItems(int paymentId)
    {
        return _paymentRepository.GetPaymentItems(paymentId);
    }
}
