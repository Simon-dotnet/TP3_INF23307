using GestBibli.Objects;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class ProductService
{
    ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public GenericResponse<Product> GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }

    public GenericResponse<List<Product>> GetAllProducts()
    {
        var result = _productRepository.GetAllProducts();
        return result;
    }

}