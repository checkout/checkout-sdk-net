using System.Net.Http;

namespace Checkout
{
    /// <summary>
    /// Default implementation of <see cref="IHttpClientFactory"/> that returns 
    /// the same <see cref="System.Net.Http.HttpClient"/> that is is initialized with.
    /// </summary>
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates a new <see cref="DefaultHttpClientFactory"/> instance.
        /// </summary>
        /// <param name="httpClient">An optional <see cref="System.Net.Http.HttpClient"/> instance to use otherwise a new one will be initialized.</param>
        public DefaultHttpClientFactory(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }
        
        /// <summary>
        /// Creates a <see cref="System.Net.Http.HttpClient"/> instance.
        /// </summary>
        /// <returns>An initialized instance.</returns>
        public HttpClient CreateClient()
        {
            return _httpClient;
        }
    }
}