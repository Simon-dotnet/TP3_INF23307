using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class ProductInStock
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int QuantityInStock { get; set; }
    
    public int MinimalQuantity { get; set; }
    
    public string StorageLocation { get; set; }
    
    public string Status { get; set; }
    
    public int Threshold { get; set; }
    
    public DateTime LastRefill { get; set; }
    
    public int ProductId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    
    public int StockId { get; set; }
    
    [ForeignKey(nameof(StockId))]
    public Stock Stock { get; set; }
}