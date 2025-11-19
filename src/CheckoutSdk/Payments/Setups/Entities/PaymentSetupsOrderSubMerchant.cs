namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsOrderSubMerchant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public long? Amount { get; set; }

        public string Reference { get; set; }
    }
}