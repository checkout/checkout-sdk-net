using System;
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
        ///     Updates a card you issued previously, sending the return-encrypted-cvv and
        ///     Encryption-Key headers. The API no longer documents these headers and
        ///     update-card-response does not carry an encrypted CVV. The headers are still sent
        ///     when set. Use <see cref="UpdateCardDetails(string, CardsUpdateRequest, CancellationToken)"/>
        ///     instead; this overload will be removed in the next major version.
        /// </summary>
        /// <param name="cardId">the card ID</param>
        /// <param name="cardUpdateRequest">the card fields to update</param>
        /// <param name="headers">the return-encrypted-cvv and Encryption-Key headers</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the card update response</returns>
        [Obsolete("The API no longer supports these headers; use the method without headers. Will be removed in the next major version.")]
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

        /// <summary>
        ///     Revokes a card. The request body is optional.
        /// </summary>
        /// <param name="cardId">the card ID</param>
        /// <param name="revokeCardRequest">the optional revocation reason; when null no body is sent</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the revoke card response links</returns>
        Task<Resource> RevokeCard(string cardId, RevokeCardRequest revokeCardRequest = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Suspends a card. The request body is optional.
        /// </summary>
        /// <param name="cardId">the card ID</param>
        /// <param name="suspendCardRequest">the optional suspension reason; when null no body is sent</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the suspend card response links</returns>
        Task<Resource> SuspendCard(string cardId, SuspendCardRequest suspendCardRequest = null,
            CancellationToken cancellationToken = default);
    }
}