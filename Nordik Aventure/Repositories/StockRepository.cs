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

    public GenericResponse<ProductInStock> GetProductInStockByProductId(int id)
    {
        try
        {
            var result = _context.ProductInStock.Where(ps => ps.Product.Id == id)
                .Include(ps => ps.Product)
                .ThenInclude(p => p.Category)
                .Include(p => p.Product)
                .ThenInclude(p => p.Supplier).FirstOrDefault();
            if (result == null)
            {
                return new GenericResponse<ProductInStock>("Le produit en stock n'existe pas encore", 404);
            }

            return new GenericResponse<ProductInStock>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<ProductInStock>("Erreur lors du chargement du produit en stock", 500);
        }
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

    public GenericResponse<ProductInStock> AddProductToStock(ProductInStock productInStock)
    {
        try
        {
            _context.ProductInStock.Add(productInStock);
            _context.SaveChanges();
            return new GenericResponse<ProductInStock>(productInStock);
        }
        catch (Exception e)
        {
            return new GenericResponse<ProductInStock>($"Erreur lors de l'ajout du produit dans le stock: {e}", 500);
        }
    }

    public GenericResponse<List<ProductInStock>> GetProductInStockToRefill()
    {
        try
        {
            var result = _context.ProductInStock.Where(ps => ps.QuantityInStock < ps.Threshold)
                .Include(ps => ps.Product).ToList();
            return new GenericResponse<List<ProductInStock>>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<List<ProductInStock>>("Erreur lors du get des produits à refill", 500);
        }
    }
}