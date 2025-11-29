using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }
    
    [Required]
    public int TransactionId { get; set; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; }
    
    [Required]
    public double Amount { get; set; }
    
    public double? RemainingBalance { get; set; }
    
    [Required]
    public string? Status { get; set; }
    
    [Required]
    public string? Type { get; set; }
}