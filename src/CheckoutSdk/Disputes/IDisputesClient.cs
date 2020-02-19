using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    /// <summary>
    /// Defines the operations available on the Checkout.com Disputes API.
    /// </summary>
    public interface IDisputesClient
    {
        /// <summary>
        /// Returns all disputes on either business or channel level.
        /// Response can be filtered via several parameters.
        /// </summary>
        /// <param name="getDisputesRequest">The payment details such as amount and curency.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        /// <returns>A task that upon completion contains the matching disputes.</returns>
        Task<GetDisputesResponse> GetDisputesAsync(GetDisputesRequest getDisputesRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
