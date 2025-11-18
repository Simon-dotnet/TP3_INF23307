namespace GestBibli.Objects.ViewModels;

public class OrderItemViewModel
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public string SupplierName { get; set; }
}