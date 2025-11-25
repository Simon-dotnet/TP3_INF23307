using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.Models.User;

public class MakeSaleViewModel
{
    public List<ProductInStock> AvailableProducts { get; set; } = new();
    public List<Client> AvailableClients{ get; set; } = new();
    public Taxes Taxes { get; set; }
}
