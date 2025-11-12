using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Required]
    public string Name { get; init; }
    
    [Required]
    public string Code { get; init; }
    
    public int Discount { get; init; }
    
    public string AverageDeliveryTime { get; init; }
    
    public IList<Product> Products { get; init; } = new List<Product>();
}