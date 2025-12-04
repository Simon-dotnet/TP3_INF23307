using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Objects.ViewModels
{
    public class SupplierReceiptViewModel
    {
        public int SupplierReceiptId { get; set; }
        public Purchase Purchase { get; set; }
        public Payment Payment { get; set; }
        public Transaction Transaction { get; set; }
        public string Status { get; set; }
        public Taxes Taxes { get; set; }
    }
}