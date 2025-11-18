using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;

namespace Nordik_Aventure.Repositories;

public class StockRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public StockRepository()
    {
    }

    public GenericResponse<Stock> GetStock()
    {
        var result = _context.Stock
            .Include(s => s.Products)
            .ThenInclude(ps => ps.Product)
            .ThenInclude(p => p.Category)
            .Include(s => s.Products)
            .ThenInclude(ps => ps.Product)
            .ThenInclude(p => p.Supplier)
            .FirstOrDefault();
        if (result == null)
        {
            return new GenericResponse<Stock>("Erreur lors du chargement du stock", 500);
        }

        return new GenericResponse<Stock>(result);
    }

    public GenericResponse<ProductInStock> GetProductStockById(int id)
    {
        var result = _context.ProductInStock.Where(ps => ps.Id == id)
            .Include(ps => ps.Product)
            .ThenInclude(p => p.Category)
            .Include(p => p.Product)
            .ThenInclude(p => p.Supplier).FirstOrDefault();
        if (result == null)
        {
            return new GenericResponse<ProductInStock>("Erreur lors du chargement du produit en stock", 500);
        }

        return new GenericResponse<ProductInStock>(result);
    }

    public GenericResponse<ProductInStock> UpdateProductInStockFromForm(ProductInStock productInStock)
    {
        try
        {
            _context.ProductInStock.Update(productInStock);
            _context.SaveChanges();
            return new GenericResponse<ProductInStock>(productInStock);
        }
        catch (Exception e)
        {
            return new GenericResponse<ProductInStock>($"Erreur lors de la sauvegarde du stock: {e}", 500);
        }
    }
}