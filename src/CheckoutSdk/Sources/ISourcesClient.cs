using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sources
{
    public interface ISourcesClient
    {
        Task<SepaSourceResponse> CreateSepaSource(SepaSourceRequest sepaSourceRequest,
            CancellationToken cancellationToken = default);
    }
}