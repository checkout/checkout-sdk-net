namespace Checkout.Payments.Setups.Entities
{
    public class OrderSubMerchant
    {
        public string Id { get; set; }

        public string ProductCategory { get; set; }

        public int? NumberOfTrades { get; set; }

        public string RegistrationDate { get; set; }
    }
}