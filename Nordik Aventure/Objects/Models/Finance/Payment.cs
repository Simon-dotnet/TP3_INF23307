using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; init; }
    
    [Required]
    public int TransactionId { get; init; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; init; }
    
    [Required]
    public double Amount { get; init; }
    
    [Required]
    public string? Status { get; init; }
    
    [Required]
    public string? Type { get; init; }
}