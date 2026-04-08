using Checkout.ComplianceRequests.Requests;
using Checkout.ComplianceRequests.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.ComplianceRequests
{
    public class ComplianceRequestsClient : AbstractClient, IComplianceRequestsClient
    {
        private const string ComplianceRequestsPath = "compliance-requests";

        public ComplianceRequestsClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<ComplianceRequestDetailsResponse> GetComplianceRequest(string paymentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Get<ComplianceRequestDetailsResponse>(
                BuildPath(ComplianceRequestsPath, paymentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<EmptyResponse> RespondToComplianceRequest(string paymentId,
            ComplianceRequestRespondRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId, "request", request);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(ComplianceRequestsPath, paymentId),
                SdkAuthorization(),
                request,
                cancellationToken);
        }
    }
}
