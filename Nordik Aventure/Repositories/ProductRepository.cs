using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;

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

}