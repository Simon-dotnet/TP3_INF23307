using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class PurchaseDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PurchaseDetailsId { get; init; }
    
    [Required]
    public int ProductId { get; init; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; init; }

    [Required]
    public int PurchaseId { get; init; }
    
    [ForeignKey(nameof(PurchaseId))]
    public Purchase Purchase { get; init; }

    [Required]
    public int Quantity { get; init; }
}