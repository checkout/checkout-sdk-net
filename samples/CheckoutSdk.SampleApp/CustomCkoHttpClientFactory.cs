using System.Net.Http;

namespace Checkout.SampleApp
{
    public class CustomCkoHttpClientFactory : IHttpClientFactory
    {
        private readonly System.Net.Http.IHttpClientFactory _msHttpClientFactory;

        public CustomCkoHttpClientFactory(System.Net.Http.IHttpClientFactory msHttpClientFactory)
        {
            _msHttpClientFactory = msHttpClientFactory;
        }
        
        public HttpClient CreateClient()
        {
            return _msHttpClientFactory.CreateClient();
        }
    }
}