namespace GestBibli.Objects.ViewModels;

public class SaleCreateViewModel
{
    public int ClientId { get; set; }
    public List<SaleItemCreateViewModel> Items { get; set; } = new List<SaleItemCreateViewModel>();
}