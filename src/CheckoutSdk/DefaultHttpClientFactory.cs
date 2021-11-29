using System.Net.Http;

namespace Checkout
{
    public sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }
}