using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class SupplierReceipt
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SupplierReceiptId { get; init; }
    
    [Required]
    public int PurchaseId { get; init; }
    
    [ForeignKey(nameof(PurchaseId))]
    public Purchase Purchase { get; init; }
    
    [Required]
    public int PaymentId { get; init; }
    
    [ForeignKey(nameof(PaymentId))]
    public Payment Payment { get; init; }
    
    [Required]
    public string? Status { get; init; }
}