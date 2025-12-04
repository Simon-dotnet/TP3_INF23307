using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class ClientBuyingHistoric
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
 
    public int SaleId { get; set; }

    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; set; }
}