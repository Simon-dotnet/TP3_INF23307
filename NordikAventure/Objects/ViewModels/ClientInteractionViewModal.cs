namespace Nordik_Aventure.Objects.ViewModels;

public class ClientInteractionViewModal
{
    public DateTime Date { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public int EmployeeId { get; set; }
    public int ClientId { get; set; }
    
    public string? ClientName { get; set; }
}