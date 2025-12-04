namespace GestBibli.Objects.ViewModels;

public class OrderItemCreateViewModel
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double TotalPrice { get; set; }
}