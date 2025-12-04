using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Sale
{
    public Sale()
    {
        SaleDetails = new List<SaleDetails>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime DateOfSale { get; set; }
    
    public double TotalPrice { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }

    [Required]
    public int TransactionId { get; set; }
    
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; }
    
    public ICollection<SaleDetails> SaleDetails { get; set; }
}