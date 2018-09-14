using System.Net.Http;

namespace Checkout
{
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public DefaultHttpClientFactory(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }
        
        public HttpClient CreateClient()
        {
            return _httpClient;
        }
    }
}