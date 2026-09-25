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
using Checkout.Issuing.Cards.Responses.Activate;
using Checkout.Issuing.Cards.Responses.Renew;
using Checkout.Issuing.Cards.Responses.Update;
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

        /// <summary>
        ///     Updates a card you issued previously.
        /// </summary>
        /// <param name="cardId">the card ID</param>
        /// <param name="cardUpdateRequest">the card fields to update</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the card update response</returns>
        Task<CardsUpdateResponse> UpdateCardDetails(string cardId,
            CardsUpdateRequest cardUpdateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Updates a card you issued previously, sending the optional return-encrypted-cvv and
        ///     Encryption-Key headers. The 2026-09-17 spec (INT-1700) removed encrypted_cvv from
        ///     update-card-response entirely, so these headers no longer make the response carry it;
        ///     the API still returns a 422 with error code encryption_key_required if
        ///     return-encrypted-cvv is set without Encryption-Key also supplied.
        /// </summary>
        /// <param name="cardId">the card ID</param>
        /// <param name="cardUpdateRequest">the card fields to update</param>
        /// <param name="headers">the optional return-encrypted-cvv and Encryption-Key headers</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the card update response</returns>
        Task<CardsUpdateResponse> UpdateCardDetails(string cardId,
            CardsUpdateRequest cardUpdateRequest,
            CardUpdateHeaders headers,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentResponse> EnrollCardThreeDS(string cardId,
            AbstractThreeDsEnrollmentRequest abstractThreeDsEnrollmentRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            AbstractThreeDsEnrollmentRequest threeDsUpdateRequest,
            CancellationToken cancellationToken = default);

        Task<ThreeDsEnrollmentDetailsResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<ActivateCardResponse> ActivateCard(string cardId,
            CancellationToken cancellationToken = default);

        Task<CardCredentialsResponse> GetCardCredentials(string cardId, 
            CardCredentialsQuery cardCredentialsQuery,
            CancellationToken cancellationToken = default);
        
        Task<RenewCardResponse> RenewCard(string cardId,
            AbstractRenewCardRequest abstractRenewCardRequest,
            CancellationToken cancellationToken = default);

        Task<Resource> RevokeCard(string cardId, RevokeCardRequest revokeCardRequest,
            CancellationToken cancellationToken = default);

        Task<Resource> SuspendCard(string cardId, SuspendCardRequest suspendCardRequest,
            CancellationToken cancellationToken = default);
    }
}