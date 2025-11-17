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
    public string Name { get; init; }
    
    [Required]
    public double PriceToBuy { get; init; }
    
    [Required]
    public double PriceToSell { get; init; }
    
    public double PaybackToSupplier { get; init; }
    
    public double Weight { get; init; }
    
    [Required]
    public string Status { get; init; }
    
    public double GrossMargin { get; init; }
    
    public int CategoryId { get; init; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; init; }
    
    public int SupplierId { get; init; }
    
    [ForeignKey(nameof(SupplierId))]
    public Supplier Supplier { get; init; }
}