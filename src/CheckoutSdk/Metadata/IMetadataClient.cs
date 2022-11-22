using Checkout.Metadata.Card;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Metadata
{
    public interface IMetadataClient
    {
        Task<CardMetadataResponse> RequestCardMetadata(CardMetadataRequest cardMetadataRequest,
            CancellationToken cancellationToken = default);
    }
}