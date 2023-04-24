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
    public partial interface IIssuingClient
    {
        Task<CardResponse> CreateCard(CardRequest cardRequest,
            CancellationToken cancellationToken = default);

        Task<CardDetailsResponse> GetCardDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<ThreeDSEnrollmentResponse> EnrollCardThreeDS(string cardId,
            ThreeDSEnrollmentRequest threeDsEnrollmentRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDSUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            ThreeDSUpdateRequest threeDsUpdateRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDSEnrollmentDetailsResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<Resource> ActivateCard(string cardId,
            CancellationToken cancellationToken = default);

        Task<CardCredentialsResponse> GetCardCredentials(string cardId, CardCredentialsQuery cardCredentialsQuery,
            CancellationToken cancellationToken = default);

        Task<Resource> RevokeCard(string cardId, RevokeCardRequest revokeCardRequest,
            CancellationToken cancellationToken = default);

        Task<Resource> SuspendCard(string cardId, SuspendCardRequest suspendCardRequest,
            CancellationToken cancellationToken = default);
    }
}