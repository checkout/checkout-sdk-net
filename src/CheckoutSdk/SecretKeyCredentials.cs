using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Checkout
{
    public class SecretKeyCredentials : IApiCredentials
    {
        private readonly CheckoutConfiguration _configuration;

        public SecretKeyCredentials(CheckoutConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task AuthorizeAsync(HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            if (string.IsNullOrEmpty(_configuration.SecretKey)) 
                throw new ArgumentException("Secret Key must be configured", nameof(_configuration.SecretKey));

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.SecretKey);
            return Task.FromResult(0);
        }
    }
}