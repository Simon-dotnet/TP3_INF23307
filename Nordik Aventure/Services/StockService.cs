using GestBibli.Objects;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class StockService
{
    StockRepository _stockRepository;

    public StockService(StockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public GenericResponse<Stock> GetStock()
    {
        return _stockRepository.GetStock();
    }

    public GenericResponse<ProductInStock> GetProductInStock(int id)
    {
        return _stockRepository.GetProductStockById(id);
    }

    public GenericResponse<ProductInStock> UpdateProductStockFromForm(ProductInStock productInStock)
    {
        return _stockRepository.UpdateProductInStockFromForm(productInStock);
    }
}