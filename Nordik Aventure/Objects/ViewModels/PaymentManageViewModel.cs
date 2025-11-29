namespace Nordik_Aventure.Objects.ViewModels;

public class PaymentManageViewModel
{
    public int PaymentId { get; set; }
    public int TransactionId { get; set; }
    public string Type { get; set; }
    public double AmountTotal { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public double? RemainingBalance { get; set; }
    public double RemainingAmountLeft => Math.Max(AmountTotal - (RemainingBalance ?? 0), 0);
}