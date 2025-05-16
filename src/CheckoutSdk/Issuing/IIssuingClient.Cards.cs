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
    public partial interface IIssuingClient
    {
        Task<AbstractCardCreateResponse> CreateCard(AbstractCardCreateRequest abstractCardCreateRequest,
            CancellationToken cancellationToken = default);

        Task<AbstractCardResponse> GetCardDetails(string cardId,
            CancellationToken cancellationToken = default);
        
        Task<UpdateResponse> UpdateCardDetails(string cardId,
            CardsUpdateRequest cardUpdateRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentResponse> EnrollCardThreeDS(string cardId,
            AbstractThreeDsEnrollmentRequest abstractThreeDsEnrollmentRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            AbstractThreeDsEnrollmentRequest threeDsUpdateRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentDetailsResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<Resource> ActivateCard(string cardId,
            CancellationToken cancellationToken = default);

        Task<CardCredentialsResponse> GetCardCredentials(string cardId, 
            CardCredentialsQuery cardCredentialsQuery,
            CancellationToken cancellationToken = default);
        
        Task<RenewCardResponse> RenewCard(string cardId,
            AbstractRenewCardRequest abstractRenewCardRequest,
            CancellationToken cancellationToken = default);

        Task<Resource> RevokeCard(string cardId, RevokeCardRequest revokeCardRequest,
            CancellationToken cancellationToken = default);
        
        Task<Resource> ScheduleCardRevocation(ScheduleCardRevocationRequest scheduleCardRevocationRequest,
            CancellationToken cancellationToken = default);
        
        Task<Resource> DeleteScheduledRevocation(string cardId,
            CancellationToken cancellationToken = default);

        Task<Resource> SuspendCard(string cardId, SuspendCardRequest suspendCardRequest,
            CancellationToken cancellationToken = default);
    }
}