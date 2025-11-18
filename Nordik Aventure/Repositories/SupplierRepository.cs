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

    public GenericResponse<Supplier> GetSupplierById(int id)
    {
        var result = _context.Suppliers.FirstOrDefault(s => s.Id == id);
        return new GenericResponse<Supplier>(result);
    }

    public GenericResponse<Supplier> AddSupplier(Supplier supplier)
    {
        try
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return new GenericResponse<Supplier>(supplier);
        }
        catch (Exception e)
        {
            return new GenericResponse<Supplier>($"Erreur lors de la sauvegarde: {e}", 500);
        }
    }
}