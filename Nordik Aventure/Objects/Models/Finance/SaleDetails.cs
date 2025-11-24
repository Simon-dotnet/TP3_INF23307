using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class SaleDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleDetailsId { get; init; }
    
    [Required]
    public int ProductStockId { get; init; }
    
    [ForeignKey(nameof(ProductStockId))]
    public ProductInStock ProductInStock { get; init; }

    [Required]
    public int SaleId { get; init; }
    
    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; init; }

    [Required]
    public int Quantity { get; init; }
}