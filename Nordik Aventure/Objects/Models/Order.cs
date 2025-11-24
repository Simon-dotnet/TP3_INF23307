using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.Finance;

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
    
    public DateTime DateOfOrdering { get; set; }
    
    public double TotalPrice { get; set; }
    
    public DateTime DateOfDelivery { get; set; }
    
    public int PurchaseId { get; init; }
    
    [ForeignKey("PurchaseId")]
    public Purchase Purchase { get; set; }
    
    public ICollection<OrderSupplierProduct> OrderSupplierProducts { get; set; }
}