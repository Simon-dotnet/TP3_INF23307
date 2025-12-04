using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class MovementHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    public string Type { get; init; }
    
    public DateTime Date { get; init; }
    
    public string? Motif { get; init; }
    
    public int? PurchaseId { get; init; }
    
    [ForeignKey("PurchaseId")]
    public Purchase? Purchase { get; init; }
    
    public int? SaleId { get; init; }
    
    [ForeignKey("SaleId")]
    public Sale? Sale { get; init; }
    
    public int EmployeeId { get; init; } 
    
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; init; }
}