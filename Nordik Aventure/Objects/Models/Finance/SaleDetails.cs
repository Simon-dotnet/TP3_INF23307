using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class SaleDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleDetailsId { get; init; }
    
    [Required]
    public int ProductId { get; init; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; init; }

    [Required]
    public int SaleId { get; init; }
    
    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; init; }

    [Required]
    public int Quantity { get; init; }
}