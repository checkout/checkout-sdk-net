namespace Checkout.Payments
{
    public class CardSourceResponse
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Address BillingAddress { get; set; }
        public Phone Phone { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Name { get; set; }
        public string Scheme { get; set; }
        public string Last4 { get; set; }
        public string Fingerprint { get; set; }
        public string Bin { get; set; }
        public string CardType { get; set; }
        public string CardCategory { get; set; }
        public string Issuer { get; set; }
        public string IssuerCountry { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }
        public string AvsCheck { get; set; }
        public string CvvCheck { get; set; }
    }
}