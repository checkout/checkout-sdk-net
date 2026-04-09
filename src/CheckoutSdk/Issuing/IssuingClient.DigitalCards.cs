using Checkout.Issuing.DigitalCards.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        private const string DigitalCardsPath = "digital-cards";

        public Task<GetDigitalCardResponse> GetDigitalCard(string digitalCardId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("digitalCardId", digitalCardId);
            return ApiClient.Get<GetDigitalCardResponse>(
                BuildPath(IssuingPath, DigitalCardsPath, digitalCardId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}
