using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Credentials class that authorizes HTTP requests using your Checkout.com Public Key.
    /// </summary>
    public class PublicKeyCredentials : IApiCredentials
    {
        private readonly CheckoutConfiguration _configuration;

        /// <summary>
        /// Creates a new see <cref="PublicKeyCredentials"/> instance.
        /// </summary>
        /// <param name="configuration">The Checkout configuration containing your public key.</param>
        public PublicKeyCredentials(CheckoutConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authorizes the request using your Checkout.com Public Key.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to authorize.</param>
        /// <returns>A task that completes when the provided <paramref="httpRequest"/> has been authorized.</returns>
        public Task AuthorizeAsync(HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            if (string.IsNullOrEmpty(_configuration.PublicKey)) 
                throw new ArgumentException("Your Public Key must be configured", nameof(_configuration.PublicKey));

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.PublicKey);
            return Task.FromResult(0);
        }
    }
}