namespace GestBibli.Objects.ViewModels;

public class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public string SupplierName { get; set; }
}