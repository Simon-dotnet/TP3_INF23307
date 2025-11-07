namespace Nordik_Aventure.Objects.Models;

public class Order
{
    int Id { get; init; }
    
    DateTime DateOfOrdering { get; init; }
    
    double TotalPrice { get; init; }
    
    DateTime DateOfDelivery { get; init; }
    
    IList<OrderSupplierProduct> ProductsInOrder { get; init; }
}