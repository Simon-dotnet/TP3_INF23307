using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class PurchaseService
{
    public PurchaseRepository _purchaseRepository;
    public PurchaseService(PurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }
    
    public GenericResponse<Purchase> GetPurchaseById(int id)
    {
        return _purchaseRepository.GetPurchaseById(id);
    }

    public GenericResponse<Purchase> AddPurchase(Purchase purchase)
    {
        return _purchaseRepository.AddPurchase(purchase);
    }
}