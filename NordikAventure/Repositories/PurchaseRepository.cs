using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class PurchaseRepository
{
    private readonly NordikAventureContext _context;

    public PurchaseRepository(NordikAventureContext context)
    {
        _context = context;
    }
    
    public GenericResponse<Purchase> GetPurchaseById(int purchaseId)
    {
        try
        {
            var result = _context.Purchases.Where(pd => pd.PurchaseId == purchaseId)
                .Include(p => p.PurchaseDetails)
                .ThenInclude(pd => pd.Product)
                .ThenInclude(p => p.Supplier).FirstOrDefault();
            return new GenericResponse<Purchase>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Purchase>("Erreur de get purchase details", 500);
        }
    }

    public GenericResponse<Purchase> AddPurchase(Purchase purchase)
    {
        try
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return new GenericResponse<Purchase>(purchase);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Purchase>($"Erreur lors de la sauvegarde d'un achat: {ex}", 500);
        }
    }
}