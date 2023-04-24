using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Enroll;
using Checkout.Issuing.Cards.Type;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public interface IIssuingClient
    {
        Task<CardholderResponse> CreateCardholder(CardholderRequest cardholderRequest,
            CancellationToken cancellationToken = default);

        Task<CardholderDetailsResponse> GetCardholderDetails(string cardholderId,
            CancellationToken cancellationToken = default);

        Task<CardholderCardsResponse> GetCardholdersCards(string cardholderId,
            CancellationToken cancellationToken = default);

        Task<CardResponse> CreateCard(CardTypeRequest cardTypeRequest,
            CancellationToken cancellationToken = default);

        Task<CardDetailsResponse> GetCardDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<CardEnrollThreeDSResponse> EnrollCardThreeDS(string cardId,
            CardEnrollThreeDSRequest cardEnrollThreeDSRequest,
            CancellationToken cancellationToken = default);

        Task<CardEnrollThreeDSDetailsUpdateResponse> UpdateCardThreeDSDetails(string cardId,
            CardEnrollThreeDSDetailsRequest cardEnrollThreeDSDetailsRequest,
            CancellationToken cancellationToken = default);

        Task<CardEnrollThreeDSDetailsGetResponse> GetCardThreeDSDetails(string cardId,
            CancellationToken cancellationToken = default);

        Task<Resource> ActivateCard(string cardId,
            CancellationToken cancellationToken = default);

        Task<CardCredentialsResponse> GetCardCredentials(string cardId, CardCredentialsQuery cardCredentialsQuery,
            CancellationToken cancellationToken = default);

        Task<Resource> RevokeCard(string cardId, CardReasonRequest cardReasonRequest,
            CancellationToken cancellationToken = default);

        Task<Resource> SuspendCard(string cardId, CardReasonRequest cardReasonRequest,
            CancellationToken cancellationToken = default);
    }
}