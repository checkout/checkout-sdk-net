namespace Checkout
{
    public class CheckoutConfiguration
    {       
        private const string ProductionUrl = "https://api.checkout.com";
        private const string SandboxUrl = "https://sandbox.checkout.com";
        
        public CheckoutConfiguration(string secretKey, bool sandbox = true) 
            : this(secretKey, sandbox ? SandboxUrl : ProductionUrl)
        {
        }
        
        public CheckoutConfiguration(string secretKey, string uri) 
        {
            
        }

        public string SecretKey { get; private set; }
        public string Uri { get; private set; }
    }
}