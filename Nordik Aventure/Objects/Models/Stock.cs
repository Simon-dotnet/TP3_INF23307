using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class Stock
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int TotalProducts { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public IList<ProductInStock> Products { get; set; }
}