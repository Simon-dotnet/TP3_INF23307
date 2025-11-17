using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class SaleReceipt
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleReceiptId { get; init; }
    
    [Required]
    public int SaleId { get; init; }
    
    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; init; }
    
    [Required]
    public int PaymentId { get; init; }
    
    [ForeignKey(nameof(PaymentId))]
    public Payment Payment { get; init; }
    
    [Required]
    public string? Status { get; init; }
}