using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public interface ITransport
    {
        Task<HttpResponseMessage> Invoke(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            HttpContent httpContent,
            CancellationToken cancellationToken,
            string idempotencyKey,
            bool useFileUri = false);
    }
}