using System;

namespace Checkout
{
    public class CheckoutConfiguration
    {
        public const string ProductionUri = "https://api.checkout.com/";
        public const string SandboxUri = "https://api.sandbox.checkout.com/";

        public CheckoutConfiguration(string secretKey, bool sandbox)
            : this(secretKey, sandbox ? SandboxUri : ProductionUri)
        {

        }

        public CheckoutConfiguration(string secretKey, string uri)
        {
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentException($"Your API secret key is required", nameof(secretKey));
            if (string.IsNullOrEmpty(uri)) throw new ArgumentException($"The API URI is required", nameof(uri));

            SecretKey = secretKey;
            Uri = uri;
        }

        public string SecretKey { get; }
        public string Uri { get; }
        public string PublicKey { get; set; }
    }
}