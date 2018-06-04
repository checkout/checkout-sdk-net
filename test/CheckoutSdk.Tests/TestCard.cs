namespace Checkout.Tests
{
    public class TestCard
    {
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }

        public static TestCard Visa => new TestCard
        {
            Number = "4242424242424242",
            ExpiryMonth = 6,
            ExpiryYear = 2018,
            Cvv = "100"
        };
    }
}