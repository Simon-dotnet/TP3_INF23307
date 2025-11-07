namespace Nordik_Aventure.Objects.Models;

public class ProductInStock
{
    int Id { get; set; }
    
    int QuantityInStock { get; set; }
    
    int MinimalQuantity { get; set; }
    
    string StorageLocation { get; set; }
    
    string Status { get; set; }
    
    int Threshold { get; set; }
    
    DateTime LastRefill { get; set; }
    
    Product Product { get; set; }
}