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
            // This HiperCard number can only be used for CI testing once the Sandbox environment contains
            // a dLocal TPA and merchant with the relevant processor setup
            // Number = "606282678627663",
            
            Number = "4242424242424242",
            ExpiryMonth = 6,
            ExpiryYear = 2025,
            Cvv = "100",
            Name = "Bill Gates"
        };
    }
}