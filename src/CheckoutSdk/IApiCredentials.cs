using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Sdk
{
    public interface IApiCredentials
    {
        Task AuthorizeAsync(HttpRequestMessage httpRequest);
    }
}