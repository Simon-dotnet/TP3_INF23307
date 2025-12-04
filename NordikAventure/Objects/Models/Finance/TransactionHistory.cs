using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class TransactionHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionHistoryId { get; init; }
    
    [Required]
    public int TransactionId { get; init; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; init; }
}