using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sources.Previous
{
    public interface ISourcesClient
    {
        Task<SepaSourceResponse> CreateSepaSource(SepaSourceRequest sepaSourceRequest,
            CancellationToken cancellationToken = default);
    }
}