using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<IssuingDisputeResponse> CreateDispute(
            CreateDisputeRequest createDisputeRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createDisputeRequest", createDisputeRequest);
            return ApiClient.Post<IssuingDisputeResponse>(
                BuildPath(IssuingPath, DisputesPath),
                SdkAuthorization(),
                createDisputeRequest,
                cancellationToken
            );
        }

        public Task<IssuingDisputeResponse> GetDisputeDetails(
            string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Get<IssuingDisputeResponse>(
                BuildPath(IssuingPath, DisputesPath, disputeId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<EmptyResponse> CancelDispute(
            string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(IssuingPath, DisputesPath, disputeId, CancelPath),
                SdkAuthorization(),
                null,
                cancellationToken,
                null
            );
        }

        public Task<EmptyResponse> EscalateDispute(
            string disputeId,
            EscalateDisputeRequest escalateDisputeRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId, "escalateDisputeRequest", escalateDisputeRequest);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(IssuingPath, DisputesPath, disputeId, EscalatePath),
                SdkAuthorization(),
                escalateDisputeRequest,
                cancellationToken
            );
        }

        public Task<IssuingDisputeResponse> SubmitDispute(
            string disputeId,
            SubmitDisputeRequest submitDisputeRequest = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<IssuingDisputeResponse>(
                BuildPath(IssuingPath, DisputesPath, disputeId, SubmitPath),
                SdkAuthorization(),
                submitDisputeRequest,
                cancellationToken,
                null
            );
        }
    }
}