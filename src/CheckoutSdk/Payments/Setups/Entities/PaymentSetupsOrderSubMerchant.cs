namespace Checkout.Payments.Setups.Requests.Entities
{
    public class PaymentSetupsOrderSubMerchant
    {
        public string Id { get; set; }

        public string ProductCategory { get; set; }

        public int? NumberOfTrades { get; set; }

        public string RegistrationDate { get; set; }
    }
}