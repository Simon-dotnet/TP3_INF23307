using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.User;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }

    [Required]
    public string EmailAddress { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public DateTime? HireDate { get; set; }
    
    [Required]
    public string Role { get; set; }
}