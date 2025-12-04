using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class SaleDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    
    public double TotalPrice { get; set; }
    
    [Required]
    public int ProductStockId { get; set; }
    
    [ForeignKey(nameof(ProductStockId))]
    public ProductInStock ProductInStock { get; set; }
    
    public int SaleId { get; set; }
    
    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; set; }
}