using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Purchase
{
    public Purchase()
    {
        PurchaseDetails = new List<PurchaseDetails>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PurchaseId { get; init; }

    [Required]
    public int TransactionId { get; init; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; init; }
    
    public ICollection<PurchaseDetails> PurchaseDetails { get; init; }
}