using Checkout.Forward.Requests;
using Checkout.Forward.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forward
{
    /// <summary>
    /// Forward
    /// </summary>
    public class ForwardClient : AbstractClient, IForwardClient
    {
        private const string Forward = "forward";
        
        public ForwardClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Forward an API request
        /// [BETA]
        /// Forwards an API request to a third-party endpoint.
        /// For example, you can forward payment credentials you've stored in our Vault to a third-party payment processor.
        /// </summary>
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

        /// <summary>
        /// Get forward request
        /// Retrieve the details of a successfully forwarded API request.
        /// The details can be retrieved for up to 14 days after the request was initiated.
        /// </summary>
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