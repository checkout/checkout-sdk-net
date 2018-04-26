namespace Checkout
{
    public class CheckoutConfiguration
    {
        private const string ProductionUrl = "https://api.checkout.com";
        private const string SandboxUrl = "https://sandbox.checkout.com";

        public CheckoutConfiguration(string secretKey, bool sandbox = true) // safe by default
        {
            SecretKey = secretKey;
            Uri = sandbox ? SandboxUrl : ProductionUrl;
        }

        public string Uri { get; set; } = ProductionUrl; // Can still be overridden for QA etc.
        public string SecretKey { get; set; }
    }
}