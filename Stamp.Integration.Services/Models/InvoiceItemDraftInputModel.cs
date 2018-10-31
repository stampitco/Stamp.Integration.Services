namespace Stamp.Integration.Services.Models
{
    public class InvoiceItemDraftInputModel
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceWithVat { get; set; }
        public decimal VatRate { get; set; }
    }
}