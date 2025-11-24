using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Objects.Models;

public class ClientBuyingHistoric
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Transaction Client { get; set; }
    
    public int TransactionId { get; set; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; }
}