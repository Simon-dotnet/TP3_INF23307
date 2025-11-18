using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models;

public class OrderSupplierProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderSupplierProductId { get; init; }
    
    public int Quantity { get; init; }
    
    public double TotalPrice { get; init; }
    
    public int SupplierId { get; init; }
    
    [ForeignKey(nameof(SupplierId))]
    public Supplier Supplier { get; init; }
    
    public int ProductId { get; init; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; init; }
    
    public int OrderId { get; init; }
    
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; init; }
}