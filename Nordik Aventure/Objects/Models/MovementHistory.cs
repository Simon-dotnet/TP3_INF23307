using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class MovementHistory
{
    int Id { get; init; }
    
    string Type { get; init; }
    
    DateTime Date { get; init; }
    
    string? Motif { get; init; }
    
    // Sell
    
    // Buy
    
    Employee OperatedByUser { get; init; }
}