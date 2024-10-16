namespace Checkout.Payments.Contexts
{
    public class PaymentContextsItems
    {
        public PaymentContextItemType? Type { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public string Reference { get; set; }

        public int TotalAmount { get; set; }

        public int TaxAmount { get; set; }

        public int DiscountAmount { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }
    }
}