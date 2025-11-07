using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Objects.Models;

public class ClientInterraction
{
    int Id { get; set; }
    
    DateTime Date { get; set; }
    
    // UserId
    
    string Type { get; set; }
    
    string Description { get; set; }
    
    Client Client { get; set; }
}