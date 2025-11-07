namespace Nordik_Aventure.Objects.Models;

public class OrderSupplierProduct
{
    int Id { get; init; }
    
    int Quantity { get; init; }
    
    double TotalPrice { get; init; } // Product.PriceToBuy * Quantity
    
    Supplier Supplier { get; init; }
    
    Product Product { get; init; }
}