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
}