using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Enroll;
using Checkout.Issuing.Cards.Type;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public class IssuingClient : AbstractClient, IIssuingClient
    {
        private const string IssuingPath = "issuing";
        private const string CardholdersPath = "cardholders";
        private const string CardsPath = "cards";
        private const string ThreeDSEnrollmentPath = "3ds-enrollment";
        private const string ActivatePath = "activate";
        private const string Credentials = "credentials";
        private const string Revoke = "revoke";
        private const string Suspend = "suspend";

        public IssuingClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<CardholderResponse> CreateCardholder(
            CardholderRequest cardholderRequest,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("cardholderRequest", cardholderRequest);
            return ApiClient.Post<CardholderResponse>(
                BuildPath(IssuingPath, CardholdersPath),
                SdkAuthorization(),
                cardholderRequest,
                cancellationToken
            );
        }

        public Task<CardholderDetailsResponse> GetCardholderDetails(
            string cardholderId,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("cardholderId", cardholderId);
            return ApiClient.Get<CardholderDetailsResponse>(
                BuildPath(IssuingPath, CardholdersPath, cardholderId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<CardholderCardsResponse> GetCardholdersCards(
            string cardholderId,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("cardholderId", cardholderId);
            return ApiClient.Get<CardholderCardsResponse>(
                BuildPath(IssuingPath, CardholdersPath, cardholderId, CardsPath),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<CardResponse> CreateCard(CardTypeRequest cardTypeRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardRequest", cardTypeRequest);
            return ApiClient.Post<CardResponse>(
                BuildPath(IssuingPath, CardsPath),
                SdkAuthorization(),
                cardTypeRequest,
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

        public Task<CardEnrollThreeDSResponse> EnrollCardThreeDS(string cardId,
            CardEnrollThreeDSRequest cardEnrollThreeDsRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardEnrollThreeDsRequest", cardEnrollThreeDsRequest);
            return ApiClient.Post<CardEnrollThreeDSResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                cardEnrollThreeDsRequest,
                cancellationToken
            );
        }

        public Task<CardEnrollThreeDSDetailsUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            CardEnrollThreeDSDetailsRequest cardEnrollThreeDSDetailsRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardEnrollThreeDSDetailsRequest",
                cardEnrollThreeDSDetailsRequest);
            return ApiClient.Patch<CardEnrollThreeDSDetailsUpdateResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                cardEnrollThreeDSDetailsRequest,
                cancellationToken
            );
        }

        public Task<CardEnrollThreeDSDetailsGetResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Get<CardEnrollThreeDSDetailsGetResponse>(
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
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Query<CardCredentialsResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, Credentials),
                SdkAuthorization(),
                cardCredentialsQuery,
                cancellationToken
            );
        }

        public Task<Resource> RevokeCard(string cardId, CardReasonRequest cardReasonRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardReasonRequest", cardReasonRequest);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, Revoke),
                SdkAuthorization(),
                cardReasonRequest,
                cancellationToken
            );
        }

        public Task<Resource> SuspendCard(string cardId, CardReasonRequest cardReasonRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardReasonRequest", cardReasonRequest);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, Suspend),
                SdkAuthorization(),
                cardReasonRequest,
                cancellationToken
            );
        }
    }
}