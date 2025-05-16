using Checkout.Issuing.Cardholders.Requests;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Common.Responses;
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
        
        Task<UpdateResponse> UpdateCardholder(string cardholderId,
            CardholderRequest cardholderRequest,
            CancellationToken cancellationToken = default);

        Task<CardholderCardsResponse> GetCardholdersCards(string cardholderId,
            CancellationToken cancellationToken = default);
    }
}