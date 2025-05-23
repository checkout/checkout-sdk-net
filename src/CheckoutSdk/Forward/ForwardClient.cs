using Checkout.Forward.Requests;
using Checkout.Forward.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forward
{
    public class ForwardClient : AbstractClient, IForwardClient
    {
        private const string Forward = "forward";
        
        public ForwardClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<ForwardAnApiResponse> ForwardAnApiRequest(ForwardRequest forwardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("forwardRequest", forwardRequest);
            return ApiClient.Post<ForwardAnApiResponse>(
                Forward,
                SdkAuthorization(),
                forwardRequest,
                cancellationToken
            );
        }

        public Task<GetForwardResponse> GetForwardRequest(string forwardId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("forwardId", forwardId);
            return ApiClient.Get<GetForwardResponse>(
                BuildPath(Forward, forwardId),
                SdkAuthorization(),
                cancellationToken
            );
        }
    }
}