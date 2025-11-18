namespace GestBibli.Objects.ViewModels;

public class OrderCreateViewModel
{
    public DateTime? DateOfDelivery { get; set; } 
    public List<OrderItemCreateViewModel> Items { get; set; } = new List<OrderItemCreateViewModel>();
}