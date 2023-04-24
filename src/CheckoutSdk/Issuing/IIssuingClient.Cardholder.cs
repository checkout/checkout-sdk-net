using Checkout.Issuing.Cardholders;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<CardholderResponse> CreateCardholder(CardholderRequest cardholderRequest,
            CancellationToken cancellationToken = default);

        Task<CardholderDetailsResponse> GetCardholderDetails(string cardholderId,
            CancellationToken cancellationToken = default);

        Task<CardholderCardsResponse> GetCardholdersCards(string cardholderId,
            CancellationToken cancellationToken = default);
    }
}