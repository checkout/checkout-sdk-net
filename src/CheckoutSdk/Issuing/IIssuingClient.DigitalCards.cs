using Checkout.Issuing.DigitalCards.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        /// <summary>
        /// Retrieve the details of a digital card.
        /// </summary>
        /// <param name="digitalCardId">The digital card identifier</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>The digital card details</returns>
        Task<GetDigitalCardResponse> GetDigitalCard(string digitalCardId,
            CancellationToken cancellationToken = default);
    }
}
