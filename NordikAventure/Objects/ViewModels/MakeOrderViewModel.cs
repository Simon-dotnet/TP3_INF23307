using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

public class MakeOrderViewModel
{
    public List<Product> AvailableProducts { get; set; } = new();
    public Taxes Taxes { get; set; }
}
