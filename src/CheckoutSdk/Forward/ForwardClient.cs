using Checkout.Common;
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

        /// <summary>
        /// Create secret
        /// Create a new secret for secure storage and retrieval.
        /// </summary>
        public Task<SecretResponse> CreateSecret(SecretRequest secretRequest, 
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("secretRequest", secretRequest);
            return ApiClient.Post<SecretResponse>(
                BuildPath(Forward, "secrets"),
                SdkAuthorization(),
                secretRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// List secrets
        /// Retrieve a list of all stored secrets.
        /// </summary>
        public Task<ItemsResponse<SecretResponse>> ListSecrets(
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<ItemsResponse<SecretResponse>>(
                BuildPath(Forward, "secrets"),
                SdkAuthorization(),
                cancellationToken
            );
        }

        /// <summary>
        /// Update secret
        /// Update an existing secret. After updating, the version is automatically incremented.
        /// Only value and entity_id can be updated.
        /// </summary>
        public Task<SecretResponse> UpdateSecret(string name, SecretRequest secretRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("name", name, "secretRequest", secretRequest);
            return ApiClient.Patch<SecretResponse>(
                BuildPath(Forward, "secrets", name),
                SdkAuthorization(),
                secretRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Delete secret
        /// Permanently delete a secret by name.
        /// </summary>
        public Task<EmptyResponse> DeleteSecret(string name,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("name", name);
            return ApiClient.Delete<EmptyResponse>(
                BuildPath(Forward, "secrets", name),
                SdkAuthorization(),
                cancellationToken
            );
        }
    }
}