using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class SupplierReceiptService
{
    private readonly SupplierReceiptRepository _supplierReceiptRepository;

    public SupplierReceiptService(SupplierReceiptRepository supplierReceiptRepository)
    {
        _supplierReceiptRepository = supplierReceiptRepository;
    }

    public GenericResponse<SupplierReceipt> AddSupplierReceipt(SupplierReceipt receipt)
    {
        return _supplierReceiptRepository.AddSupplierReceipt(receipt);
    }

    public GenericResponse<SupplierReceipt> GetById(int id)
    {
        return _supplierReceiptRepository.GetById(id);
    }

    public GenericResponse<SupplierReceipt> GetByTransactionId(int transactionId)
    {
        return _supplierReceiptRepository.GetByTransactionId(transactionId);
    }
}