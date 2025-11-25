using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class SaleRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public SaleRepository()
    {
        
    }
    
    public GenericResponse<Sale> GetSaleById(int saleId)
    {
        try
        {
            var result = _context.Sales
                .Where(s => s.Id == saleId)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.ProductInStock)
                .ThenInclude(pis => pis.Product)
                .Include(s => s.SaleDetails)
                .Include(s => s.Client)
                .FirstOrDefault();

            return new GenericResponse<Sale>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Sale>("Erreur de get sale details", 500);
        }
    }
    
    public GenericResponse<Sale> AddSale(Sale sale)
    {
        try
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
            return new GenericResponse<Sale>(sale);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Sale>($"Erreur lors de la sauvegarde d'une vente client: {ex}", 500);
        }
    }
}