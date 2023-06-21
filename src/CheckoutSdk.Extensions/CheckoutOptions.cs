using Checkout;
using System;
using System.Net.Http;
using Environment = Checkout.Environment;

namespace CheckoutSDK.Extensions.Configuration
{
    public class CheckoutOptions
    {
        public string SecretKey { get; set; }

        public string PublicKey { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri AuthorizationUri { get; set; }

        public OAuthScope[] Scopes { get; set; }

        public Environment Environment { get; set; }

        public PlatformType? PlatformType { get; set; }

        public HttpClient HttpClient { get; set; }
        
    }
}