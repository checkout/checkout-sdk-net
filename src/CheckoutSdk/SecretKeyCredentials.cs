using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Credentials class that authorizes HTTP requests using your Checkout.com Secret Key.
    /// </summary>
    public class SecretKeyCredentials : IApiCredentials
    {
        private readonly CheckoutConfiguration _configuration;

        /// <summary>
        /// Creates a new see <cref="SecretKeyCredentials"/> instance.
        /// </summary>
        /// <param name="configuration">The Checkout configuration containing your secret key.</param>
        public SecretKeyCredentials(CheckoutConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authorizes the request using your Checkout.com Secret Key.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to authorize.</param>
        /// <returns>A task that completes when the provided <paramref="httpRequest"/> has been authorized.</returns>
        public Task AuthorizeAsync(HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            if (string.IsNullOrEmpty(_configuration.SecretKey)) 
                throw new ArgumentException("Your Secret Key must be configured", nameof(_configuration.SecretKey));

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.SecretKey);
            return Task.FromResult(0);
        }
    }
}