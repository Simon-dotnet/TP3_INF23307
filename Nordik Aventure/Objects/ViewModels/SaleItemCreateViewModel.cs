namespace GestBibli.Objects.ViewModels;

public class SaleItemCreateViewModel
{
    public int ProductStockId { get; set; }
    public int Quantity { get; set; }
    
    public double UnitPrice { get; set; }
    public double TotalPrice { get; set; }
}