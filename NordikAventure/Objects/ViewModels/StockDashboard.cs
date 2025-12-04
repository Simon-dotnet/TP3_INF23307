using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

namespace GestBibli.Objects.ViewModels;

public class StockDashboard
{
    public List<ProductInStock> ProductInStockToRefill { get; init; }
    
    public List<Sale> SalesOfTheWeek { get; init; }
    
    public double ProfitOfTheWeek { get; init; }
}