namespace Nordik_Aventure.Objects.ViewModels;

public class OrderManageViewModel
{
    public int OrderId { get; set; }
    public string SupplierName { get; set; }
    public string Status { get; set; }
    public DateTime DateOfOrdering { get; set; }
    public DateTime DateOfDelivery { get; set; }
    public double TotalPrice { get; set; }
}