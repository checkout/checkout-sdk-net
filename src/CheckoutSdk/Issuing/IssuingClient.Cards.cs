using Checkout.Common;
using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Responses;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<CardResponse> CreateCard(CardRequest cardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardRequest", cardRequest);
            return ApiClient.Post<CardResponse>(
                BuildPath(IssuingPath, CardsPath),
                SdkAuthorization(),
                cardRequest,
                cancellationToken
            );
        }

        public Task<CardDetailsResponse> GetCardDetails(string cardId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Get<CardDetailsResponse>(
                BuildPath(IssuingPath, CardsPath, cardId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<ThreeDSEnrollmentResponse> EnrollCardThreeDS(string cardId,
            ThreeDSEnrollmentRequest threeDsEnrollmentRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "threeDsEnrollmentRequest", threeDsEnrollmentRequest);
            return ApiClient.Post<ThreeDSEnrollmentResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                threeDsEnrollmentRequest,
                cancellationToken
            );
        }

        public Task<ThreeDSUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            ThreeDSUpdateRequest threeDsUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "threeDsUpdateRequest",
                threeDsUpdateRequest);
            return ApiClient.Patch<ThreeDSUpdateResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                threeDsUpdateRequest,
                cancellationToken
            );
        }

        public Task<ThreeDSEnrollmentDetailsResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Get<ThreeDSEnrollmentDetailsResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<Resource> ActivateCard(string cardId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, ActivatePath),
                SdkAuthorization(),
                null,
                cancellationToken,
                null
            );
        }

        public Task<CardCredentialsResponse> GetCardCredentials(string cardId,
            CardCredentialsQuery cardCredentialsQuery,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardCredentialsQuery", cardCredentialsQuery);
            return ApiClient.Query<CardCredentialsResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, Credentials),
                SdkAuthorization(),
                cardCredentialsQuery,
                cancellationToken
            );
        }

        public Task<Resource> RevokeCard(string cardId, RevokeCardRequest revokeCardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "revokeCardRequest", revokeCardRequest);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, Revoke),
                SdkAuthorization(),
                revokeCardRequest,
                cancellationToken
            );
        }

        public Task<Resource> SuspendCard(string cardId, SuspendCardRequest suspendCardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "suspendCardRequest", suspendCardRequest);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, Suspend),
                SdkAuthorization(),
                suspendCardRequest,
                cancellationToken
            );
        }
    }
}