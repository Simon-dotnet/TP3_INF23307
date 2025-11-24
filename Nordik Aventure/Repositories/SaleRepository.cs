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
            var result = _context.Sales.Where(s => s.SaleId == saleId)
                .Include(s => s.SaleDetails)
                .ThenInclude(ps => ps.ProductInStock)
                .Include(s => s.Client).FirstOrDefault();
            return new GenericResponse<Sale>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Sale>("Erreur de get sale details", 500);
        }
    }
}