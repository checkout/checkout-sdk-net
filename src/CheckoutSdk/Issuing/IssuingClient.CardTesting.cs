using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<CardAuthorizationResponse> SimulateAuthorization(
            CardAuthorizationRequest cardAuthorizationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardAuthorizationRequest", cardAuthorizationRequest);
            return ApiClient.Post<CardAuthorizationResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath),
                SdkAuthorization(),
                cardAuthorizationRequest,
                cancellationToken
            );
        }

        public Task<CardIncrementAuthorizationResponse> SimulateIncrementingAuthorization(
            string authorizationId,
            CardIncrementAuthorizationRequest cardIncrementAuthorizationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("authorizationId", authorizationId, "cardIncrementAuthorizationRequest",
                cardIncrementAuthorizationRequest);
            return ApiClient.Post<CardIncrementAuthorizationResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath, authorizationId, AuthorizationPath),
                SdkAuthorization(),
                cardIncrementAuthorizationRequest,
                cancellationToken
            );
        }

        public Task<EmptyResponse> SimulateClearing(
            string authorizationId,
            CardClearingAuthorizationRequest cardClearingAuthorizationRequest,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("authorizationId", authorizationId, "cardClearingAuthorizationRequest",
                cardClearingAuthorizationRequest);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath, authorizationId, PresentmentsPath),
                SdkAuthorization(),
                cardClearingAuthorizationRequest,
                cancellationToken
            );
        }

        public Task<EmptyResponse> SimulateRefund(
            string authorizationId,
            CardRefundAuthorizationRequest cardRefundAuthorizationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("authorizationId", authorizationId, "cardRefundAuthorizationRequest",
                cardRefundAuthorizationRequest);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath, authorizationId, RefundsPath),
                SdkAuthorization(),
                cardRefundAuthorizationRequest,
                cancellationToken
            );
        }

        public Task<CardReversalAuthorizationResponse> SimulateReversal(
            string authorizationId,
            CardReversalAuthorizationRequest cardReversalAuthorizationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("authorizationId", authorizationId, "cardReversalAuthorizationRequest",
                cardReversalAuthorizationRequest);
            return ApiClient.Post<CardReversalAuthorizationResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath, authorizationId, ReversalsPath),
                SdkAuthorization(),
                cardReversalAuthorizationRequest,
                cancellationToken
            );
        }
    }
}