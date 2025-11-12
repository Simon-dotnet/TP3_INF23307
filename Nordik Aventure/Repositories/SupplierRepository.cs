using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;

namespace Nordik_Aventure.Repositories;

public class SupplierRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public SupplierRepository()
    {
        
    }

    public GenericResponse<List<Supplier>> GetAllSuppliersWithItsProducts()
    {
        var result = _context.Suppliers.Include(s => s.Products).ToList();
        return new GenericResponse<List<Supplier>>(result);
    }
}