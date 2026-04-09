using Checkout.ComplianceRequests.Requests;
using Checkout.ComplianceRequests.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.ComplianceRequests
{
    public interface IComplianceRequestsClient
    {
        /// <summary>
        /// Retrieve an existing compliance request by payment ID.
        /// </summary>
        /// <param name="paymentId">The compliance request's payment identifier</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>The compliance request details</returns>
        Task<ComplianceRequestDetailsResponse> GetComplianceRequest(string paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Submit a response to a compliance request.
        /// </summary>
        /// <param name="paymentId">The compliance request's payment identifier</param>
        /// <param name="request">The response payload</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>An empty response on success</returns>
        Task<EmptyResponse> RespondToComplianceRequest(string paymentId,
            ComplianceRequestRespondRequest request,
            CancellationToken cancellationToken = default);
    }
}
