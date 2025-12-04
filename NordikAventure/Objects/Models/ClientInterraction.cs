using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.User;
using Nordik_Aventure.Objects.Models;

namespace Nordik_Aventure.Objects.Models;

public class ClientInterraction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string? Type { get; set; }
    
    public string? Description { get; set; }
    
    public int EmployeeId { get; set; }
    
    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; }
    
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
}