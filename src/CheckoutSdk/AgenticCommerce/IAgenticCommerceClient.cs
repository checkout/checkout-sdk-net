using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.AgenticCommerce
{
    public interface IAgenticCommerceClient
    {
        /// <summary>
        /// Create a delegated payment token to securely enable agentic commerce transactions.
        /// The token secures cardholder credentials for use in agentic payments.
        /// </summary>
        /// <remarks>
        /// The request must include a valid HMAC-SHA256 signature in the <c>Signature</c> header
        /// and a timestamp in the <c>Timestamp</c> header to verify request integrity.
        /// </remarks>
        /// <param name="request">The delegated payment request</param>
        /// <param name="headers">The required authentication headers (Signature and Timestamp)</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>The created delegated payment token</returns>
        Task<DelegatedPaymentResponse> CreateDelegatedPaymentToken(DelegatedPaymentRequest request,
            DelegatedPaymentHeaders headers,
            CancellationToken cancellationToken = default);
    }
}
