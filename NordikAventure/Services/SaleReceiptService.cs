using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class SaleReceiptService
{
    private readonly SaleReceiptRepository _saleReceiptRepository;

    public SaleReceiptService(SaleReceiptRepository saleReceiptRepository)
    {
        _saleReceiptRepository = saleReceiptRepository;
    }

    public GenericResponse<SaleReceipt> AddSaleReceipt(SaleReceipt receipt)
    {
        return _saleReceiptRepository.AddSaleReceipt(receipt);
    }

    public GenericResponse<SaleReceipt> GetById(int id)
    {
        return _saleReceiptRepository.GetById(id);
    }

    public GenericResponse<SaleReceipt> GetBySaleId(int saleId)
    {
        return _saleReceiptRepository.GetBySaleId(saleId);
    }
    
    public GenericResponse<SaleReceipt> GetByTransactionId(int transactionId)
    {
        return _saleReceiptRepository.GetByTransactionId(transactionId);
    }
}