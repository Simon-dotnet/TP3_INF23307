using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.User;

namespace GestBibli.Objects.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Sku { get; init; }
    
    public string Name { get; init; }
    
    public double PriceToBuy { get; init; }
    
    public double PriceToSell { get; init; }
    
    public double PaybackToSupplier { get; init; }
    
    public double Weight { get; init; }
    
    public string Status { get; init; }
    
    public List<Category> Categories { get; init; }
 
    public List<Supplier> Suppliers { get; init; }
    
    public int SelectedCategoryId { get; init; }
    
    public int SelectedSupplierId { get; init; }
}