namespace Checkout.Apm.Klarna
{
    public sealed class KlarnaProduct
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? UnitPrice { get; set; }

        public long? TaxRate { get; set; }

        public long? TotalAmount { get; set; }

        public long? TotalTaxAmount { get; set; }
    }
}