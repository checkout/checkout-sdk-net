using System.Net.Http;

namespace Checkout.Sdk
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient(); 
    }
}