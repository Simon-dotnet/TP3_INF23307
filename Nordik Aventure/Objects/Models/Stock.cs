namespace Nordik_Aventure.Objects.Models;

public class Stock
{
    int TotalProducts { get; set; } // Total de ProductInStock.QuantityInStock
    
    DateTime LastUpdate { get; set; }
    
    IList<ProductInStock> Products { get; set; }
}