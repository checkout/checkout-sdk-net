using Checkout.Common;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Renew;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
using Checkout.Issuing.Cards.Responses.Renew;
using Checkout.Issuing.Common.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<AbstractCardCreateResponse> CreateCard(AbstractCardCreateRequest abstractCardCreateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardRequest", abstractCardCreateRequest);
            return ApiClient.Post<AbstractCardCreateResponse>(
                BuildPath(IssuingPath, CardsPath),
                SdkAuthorization(),
                abstractCardCreateRequest,
                cancellationToken
            );
        }

        public Task<AbstractCardResponse> GetCardDetails(string cardId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Get<AbstractCardResponse>(
                BuildPath(IssuingPath, CardsPath, cardId),
                SdkAuthorization(),
                cancellationToken
            );
        }
        
        public Task<UpdateResponse> UpdateCardDetails(string cardId,
            CardsUpdateRequest cardUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "cardUpdateRequest", cardUpdateRequest);
            return ApiClient.Patch<UpdateResponse>(
                BuildPath(IssuingPath, CardsPath, cardId),
                SdkAuthorization(),
                cardUpdateRequest,
                cancellationToken
            );
        }

        public Task<ThreeDsEnrollmentResponse> EnrollCardThreeDS(string cardId,
            AbstractThreeDsEnrollmentRequest abstractThreeDsEnrollmentRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "threeDsEnrollmentRequest", abstractThreeDsEnrollmentRequest);
            return ApiClient.Post<ThreeDsEnrollmentResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                abstractThreeDsEnrollmentRequest,
                cancellationToken
            );
        }

        public Task<ThreeDsEnrollmentUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            AbstractThreeDsEnrollmentRequest threeDsUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "threeDsUpdateRequest",
                threeDsUpdateRequest);
            return ApiClient.Patch<ThreeDsEnrollmentUpdateResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, ThreeDSEnrollmentPath),
                SdkAuthorization(),
                threeDsUpdateRequest,
                cancellationToken
            );
        }

        public Task<ThreeDsEnrollmentDetailsResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Get<ThreeDsEnrollmentDetailsResponse>(
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

        public Task<RenewCardResponse> RenewCard(string cardId, AbstractRenewCardRequest abstractRenewCardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId, "abstractRenewCardRequest", abstractRenewCardRequest);
            return ApiClient.Post<RenewCardResponse>(
                BuildPath(IssuingPath, CardsPath, cardId, Renew),
                SdkAuthorization(),
                abstractRenewCardRequest,
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

        public Task<Resource> ScheduleCardRevocation(
            ScheduleCardRevocationRequest scheduleCardRevocationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("scheduleCardRevocationRequest", scheduleCardRevocationRequest);
            return ApiClient.Post<Resource>(
                BuildPath(IssuingPath, CardsPath, ScheduleRevocation),
                SdkAuthorization(),
                scheduleCardRevocationRequest,
                cancellationToken
            );
        }

        public Task<Resource> DeleteScheduledRevocation(string cardId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardId", cardId);
            return ApiClient.Delete<Resource>(
                BuildPath(IssuingPath, CardsPath, cardId, ScheduleRevocation),
                SdkAuthorization(),
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