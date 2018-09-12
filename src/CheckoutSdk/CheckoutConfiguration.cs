using System;

namespace Checkout.Sdk
{
    public class CheckoutConfiguration
    {
        private const string ProductionUrl = "https://api2.checkout.com/";
        private const string SandboxUrl = "https://sandbox.checkout.com/api2/";

        public CheckoutConfiguration(string secretKey, bool sandbox)
            : this(secretKey, sandbox ? SandboxUrl : ProductionUrl)
        {

        }

        public CheckoutConfiguration(string secretKey, string uri)
        {
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentException($"{secretKey} null or empty", nameof(secretKey));
            if (string.IsNullOrEmpty(uri)) throw new ArgumentException($"{nameof(uri)} null or empty", nameof(uri));

            SecretKey = secretKey;
            Uri = uri;
        }

        public string SecretKey { get; }
        public string Uri { get; }
        public string PublicKey { get; set; }
    }
}