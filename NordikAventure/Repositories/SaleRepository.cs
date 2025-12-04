using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class SaleRepository
{
    private readonly NordikAventureContext _context;

    public SaleRepository(NordikAventureContext context)
    {
        _context = context;
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

    public GenericResponse<List<Sale>> GetSalesOfTheWeek()
    {
        var result = _context.Sales
            .Where(s => s.DateOfSale >= DateTime.Now.AddDays(-7))
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.ProductInStock)
            .ThenInclude(ps => ps.Product)
            .OrderByDescending(s => s.DateOfSale)
            .ToList();
        return new GenericResponse<List<Sale>>(result);
    }

    public bool ClientHasSales(int clientId)
    {
        return _context.Sales.Any(s => s.ClientId == clientId);
    }

    public GenericResponse<List<Sale>> GetSaleByClient(int id)
    {
        try
        {
            var result = _context.Sales
                .Where(s => s.ClientId == id)
                .Include(sale => sale.Client)
                .Include(sale => sale.SaleDetails)
                .ThenInclude(sd => sd.ProductInStock)
                .ThenInclude(pis => pis.Product)
                .ThenInclude(p => p.Supplier)
                .OrderByDescending(p => p.DateOfSale)
                .ToList();
            return new GenericResponse<List<Sale>>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<List<Sale>>($"Erreur lors du get des ventes d'un client: {ex.Message}", 500);
        }
    }
}