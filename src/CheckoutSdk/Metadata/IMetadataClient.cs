using Checkout.Metadata.Card;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Metadata
{
    public interface IMetadataClient
    {
        /// <summary>
        /// Returns metadata for the card specified by the PAN, BIN, token, or instrument supplied.
        /// </summary>
        Task<CardMetadataResponse> RequestCardMetadata(CardMetadataRequest cardMetadataRequest,
            CancellationToken cancellationToken = default);
    }
}
