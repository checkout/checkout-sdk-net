namespace Checkout.Apm.Klarna
{
    public class PaymentMethodCategory
    {
        public string Identifier { get; set; }

        public string Name { get; set; }

        public PaymentMethodCategoryAssetUrl AssetUrls { get; set; }
    }
}