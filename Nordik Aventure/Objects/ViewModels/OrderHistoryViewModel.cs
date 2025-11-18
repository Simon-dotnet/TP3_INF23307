namespace GestBibli.Objects.ViewModels;

public class OrderHistoryViewModel
{
    public int OrderId { get; set; }
    public DateTime DateOfOrdering { get; set; }
    public DateTime DateOfDelivery { get; set; }
    public double TotalPrice { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}