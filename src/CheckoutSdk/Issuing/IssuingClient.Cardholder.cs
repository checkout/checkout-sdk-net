using Checkout.Issuing.Cardholders;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
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
    }
}