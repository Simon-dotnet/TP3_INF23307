using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; init; }
    
    [Required]
    public string? Type { get; init; }
    
    [Required]
    public double Amount { get; init; }
    
    [Required]
    public double AmountTps { get; init; }
    
    [Required]
    public double AmountTvq { get; init; }
    
    [Required]
    public double AmountTotal  { get; init; }

    [Required]
    public DateTime Date { get; init; }
}