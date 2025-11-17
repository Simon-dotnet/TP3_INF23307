using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; init; }
    
    DateTime DateOfOrdering { get; init; }
    
    double TotalPrice { get; init; }
    
    DateTime DateOfDelivery { get; init; }
    
    IList<OrderSupplierProduct> ProductsInOrder { get; init; }
}