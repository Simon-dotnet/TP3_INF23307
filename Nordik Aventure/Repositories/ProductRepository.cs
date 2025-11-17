using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Repositories;

public class ProductRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public ProductRepository()
    {
        
    }


    public GenericResponse<Product> GetProductById(int id)
    {
        var product = _context.Products.Where(product => product.Id == id)
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .SingleOrDefault();

        return new GenericResponse<Product>(product);
    }

    public GenericResponse<List<Product>> GetAllProducts()
    {
        var products = _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category).ToList();
        return new GenericResponse<List<Product>>(products);
    }

    public GenericResponse<Product> AddProduct(Product product)
    {
        try
        {
            var existingSku = _context.Products.FirstOrDefault(p => p.Sku == product.Sku);

            if (existingSku != null)
            {
                return new GenericResponse<Product>("Sku existe déjà", 400);
            }
            
            _context.Products.Add(product);
            _context.SaveChanges();
            return new GenericResponse<Product>(product);
        }
        catch (Exception e)
        {
            return new GenericResponse<Product>($"error:{e}", 500);
        }
       
    }

    public GenericResponse<List<Category>> GetAllCategories()
    {
        return new GenericResponse<List<Category>>(_context.Categories.ToList());
    }
}