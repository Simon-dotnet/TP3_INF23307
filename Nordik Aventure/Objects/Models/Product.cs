using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class Product
{
    int Id { get; init; }
    
    string Sku { get; init; }
    
    string Name { get; init; }
    
    double PriceToBuy { get; init; }
    
    double PriceToSell { get; init; }
    
    int PaybackToSupplier { get; init; }
    
    double Weight { get; init; }
    
    string Status { get; init; }
    
    IList<Category> Categories { get; init; }
    
    // Supplier Supplier
}