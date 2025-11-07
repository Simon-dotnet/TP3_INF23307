namespace Nordik_Aventure.Objects.Models;

public class Supplier
{
    int Id { get; init; }
    
    string Name { get; init; }
    
    string Code { get; init; }
    
    int Discount { get; init; }
    
    string AverageDeliveryTime { get; init; }
    
    IList<Product> Products { get; init; } = new List<Product>();
}