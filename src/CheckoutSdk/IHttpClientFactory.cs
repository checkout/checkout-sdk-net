using System.Net.Http;

namespace Checkout
{
    public interface IHttpClientFactory
    {
        HttpClient Create(); 
    }
}