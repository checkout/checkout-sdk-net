using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<CardAuthorizationResponse> SimulateAuthorization(
            CardAuthorizationRequest cardAuthorizationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardAuthorizationRequest", cardAuthorizationRequest);
            return ApiClient.Post<CardAuthorizationResponse>(
                BuildPath(IssuingPath, SimulatePath, AuthorizationPath),
                SdkAuthorization(),
                cardAuthorizationRequest,
                cancellationToken
            );
        }
    }
}