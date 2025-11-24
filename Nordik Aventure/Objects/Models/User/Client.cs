using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.User;

public class Client
{
    public Client()
    {
        ClientInterraction = new List<ClientInterraction>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Microsoft.Build.Framework.Required]
    public string Name { get; init; }
    
    [Microsoft.Build.Framework.Required]
    public string Address { get; init; }
    
    public string? Phone { get; init; }
    
    [Microsoft.Build.Framework.Required]
    public string Email { get; init; }
    
    [Microsoft.Build.Framework.Required]
    public string Password { get; init; }
    
    [Microsoft.Build.Framework.Required]
    public string Type { get; init; }
    
    public string? Status { get; init; }
    
    public string? SatisfactionLevel { get; init; }
    
    public ICollection<ClientInterraction> ClientInterraction { get; set; }
    
    // TotalVente
}