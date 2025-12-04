namespace GestBibli.Objects.ViewModels;

public class OrderHistoryViewModel
{
    public int OrderId { get; set; }
    public DateTime DateOfOrdering { get; set; }
    public DateTime DateOfDelivery { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
    public string SupplierName { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}