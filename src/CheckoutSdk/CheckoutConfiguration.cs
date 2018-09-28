using System;

namespace Checkout
{
    /// <summary>
    /// Defines the configuration used to connect and authenticate to Checkout APIs.
    /// </summary>
    public class CheckoutConfiguration
    {
        public const string ProductionUri = "https://api.checkout.com/";
        public const string SandboxUri = "https://api.sandbox.checkout.com/";

        /// <summary>
        /// Creates a new <see cref="CheckoutConfiguration"/> instance.
        /// </summary>
        /// <param name="secretKey">Your secret key obtained from the Checkout Hub.</param>
        /// <param name="useSandbox">Whether to connect to the Checkout Sandbox. False indicates the live environment should be used.</param>
        public CheckoutConfiguration(string secretKey, bool useSandbox)
            : this(secretKey, useSandbox ? SandboxUri : ProductionUri)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CheckoutConfiguration"/> instance, explicitly setting the API's base URI. 
        /// </summary>
        /// <param name="secretKey">Your secret key obtained from the Checkout Hub.</param>
        /// <param name="uri">The base URL of the Checkout API you wish to connect to.</param>
        public CheckoutConfiguration(string secretKey, string uri)
        {
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentException($"Your API secret key is required", nameof(secretKey));
            if (string.IsNullOrEmpty(uri)) throw new ArgumentException($"The API URI is required", nameof(uri));

            SecretKey = secretKey;
            Uri = uri;
        }

        /// <summary>
        /// Gets the secret key that will be used to authenticate to the Checkout API.
        /// </summary>
        public string SecretKey { get; }
        
        /// <summary>
        /// Gets the Uri of the Checkout API to connect to.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Gets or sets your public key as obtained from the Checkout Hub.
        /// </summary>
        public string PublicKey { get; set; }
    }
}