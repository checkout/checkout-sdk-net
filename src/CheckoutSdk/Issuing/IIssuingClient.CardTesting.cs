using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<CardAuthorizationResponse> SimulateAuthorization(CardAuthorizationRequest cardAuthorizationRequest,
            CancellationToken cancellationToken = default);
        
        Task<CardIncrementAuthorizationResponse> SimulateIncrementingAuthorization(string authorizationId, CardIncrementAuthorizationRequest cardIncrementAuthorizationRequest,
            CancellationToken cancellationToken = default);
        
        Task<EmptyResponse> SimulateClearing(string authorizationId, CardClearingAuthorizationRequest cardClearingAuthorizationRequest,
            CancellationToken cancellationToken = default);
        
        Task<EmptyResponse> SimulateRefund(string authorizationId, CardRefundAuthorizationRequest cardRefundAuthorizationRequest,
            CancellationToken cancellationToken = default);
        
        Task<CardReversalAuthorizationResponse> SimulateReversal(string authorizationId, CardReversalAuthorizationRequest cardReversalAuthorizationRequest,
            CancellationToken cancellationToken = default);
    }
}