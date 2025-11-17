using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; init; }
    
    [Required]
    public int ClientId { get; init; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; init; }

    [Required]
    public int TransactionId { get; init; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; init; }
}