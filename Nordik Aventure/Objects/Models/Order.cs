using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class Order
{
    public Order()
    {
        OrderSupplierProducts = new List<OrderSupplierProduct>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; init; }
    
    public DateTime DateOfOrdering { get; init; }
    
    public double TotalPrice { get; init; }
    
    public DateTime DateOfDelivery { get; init; }
    
    public ICollection<OrderSupplierProduct> OrderSupplierProducts { get; init; }
}