using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiCredentials
    {
        Task AuthorizeAsync(HttpRequestMessage httpRequest);
    }
}