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
    }
}