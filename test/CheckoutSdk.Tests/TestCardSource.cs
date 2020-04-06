namespace Checkout.Tests
{
    public class TestCardSource
    {
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public string Name { get; set; }

        public static TestCardSource Visa => new TestCardSource
        {
            Number = "4242424242424242",
            ExpiryMonth = 6,
            ExpiryYear = 2025,
            Cvv = "100"
        };

        public static TestCardSource HiperCard => new TestCardSource
        {
            Number = "606282678627663",
            ExpiryMonth = 9,
            ExpiryYear = 2035,
            Cvv = "123",
            Name = "Bill Gates"
        };
    }
}