using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Required]
    public string Sku { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public double PriceToBuy { get; set; }
    
    [Required]
    public double PriceToSell { get; set; }
    
    public double PaybackToSupplier { get; set; }
    
    public double Weight { get; set; }
    
    [Required]
    public string Status { get; set; }
    
    public double GrossMargin { get; set; }
    
    public string Description { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    
    public int SupplierId { get; set; }
    
    [ForeignKey(nameof(SupplierId))]
    public Supplier Supplier { get; set; }
}