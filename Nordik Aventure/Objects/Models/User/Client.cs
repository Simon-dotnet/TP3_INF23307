using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.User;

public class Client
{
    public Client()
    {
        ClientInterraction = new List<ClientInterraction>();
        ClientBuyingHistoric = new List<ClientBuyingHistoric>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    public string? Phone { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    public string? Status { get; set; }
    
    public int? SatisfactionLevel { get; set; }
    
    public ICollection<ClientInterraction> ClientInterraction { get; set; }
    
    public ICollection<ClientBuyingHistoric> ClientBuyingHistoric { get; set; }
}