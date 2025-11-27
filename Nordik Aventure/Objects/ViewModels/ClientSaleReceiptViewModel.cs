using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Objects.ViewModels;

public class ClientSaleReceiptViewModel
{
    public int SaleReceiptId { get; set; }
    public Sale Sale { get; set; }
    public Payment Payment { get; set; }
    public Transaction Transaction { get; set; }
    public Taxes Taxes { get; set; }
}