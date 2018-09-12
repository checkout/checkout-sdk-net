using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Checkout.Sdk
{
    public class PublicKeyCredentials : IApiCredentials
    {
        private readonly CheckoutConfiguration _configuration;

        public PublicKeyCredentials(CheckoutConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task AuthorizeAsync(HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            if (string.IsNullOrEmpty(_configuration.PublicKey)) 
                throw new ArgumentException("Public Key must be configured", nameof(_configuration.PublicKey));

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.PublicKey);
            return Task.FromResult(0);
        }
    }
}