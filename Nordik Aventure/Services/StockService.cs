using GestBibli.Objects;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class StockService
{
    private readonly StockRepository _stockRepository;
    private readonly SaleRepository _saleRepository;
    private readonly PurchaseRepository _purchaseRepository;

    public StockService(StockRepository stockRepository, SaleRepository saleRepository, PurchaseRepository purchaseRepository)
    {
        _stockRepository = stockRepository;
        _saleRepository = saleRepository;
        _purchaseRepository = purchaseRepository;
    }

    public GenericResponse<Stock> GetStock()
    {
        var stock = _stockRepository.GetStock();
        stock.Data.LastUpdate = stock.Data.Products.Max(p => p.LastRefill);
        stock.Data.TotalProducts = stock.Data.Products.Sum(p => p.QuantityInStock);
        return stock;
    }

    public GenericResponse<ProductInStock> GetProductInStock(int id)
    {
        return _stockRepository.GetProductStockById(id);
    }

    public GenericResponse<ProductInStock> UpdateProductStockFromForm(ProductInStock productInStock)
    {
        return _stockRepository.UpdateProductInStockFromForm(productInStock);
    }

    public GenericResponse<ProductInStock> GetProductInStockFromProductId(int id)
    {
        return _stockRepository.GetProductInStockByProductId(id);
    }

    public GenericResponse<ProductInStock> AddProductToStock(ProductInStock productInStock)
    {
        return _stockRepository.AddProductToStock(productInStock);
    }

    public GenericResponse<List<ProductInStock>> GetProductInStockToRefill()
    {
        return _stockRepository.GetProductInStockToRefill();
    }

    // public GenericResponse<double> CalculateProfitOfTheWeek()
    // {
    //     var saleOfTheWeek = _saleRepository.GetSaleOfTheWeek();
    //     var purchaseOfTheWeek = 
    // }
}