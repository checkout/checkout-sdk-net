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
    public interface IForwardClient
    {
        /// <summary>
        /// Forward an API request
        /// [BETA]
        /// Forwards an API request to a third-party endpoint.
        /// For example, you can forward payment credentials you've stored in our Vault to a third-party payment processor.
        /// </summary>
        Task<ForwardAnApiResponse> ForwardAnApiRequest(ForwardRequest forwardRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get forward request
        /// Retrieve the details of a successfully forwarded API request.
        /// The details can be retrieved for up to 14 days after the request was initiated.
        /// </summary>
        Task<GetForwardResponse> GetForwardRequest(string forwardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create secret
        /// Create a new secret for secure storage and retrieval.
        /// </summary>
        Task<SecretResponse> CreateSecret(SecretRequest secretRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// List secrets
        /// Retrieve a list of all stored secrets.
        /// </summary>
        Task<ItemsResponse<SecretResponse>> ListSecrets(CancellationToken cancellationToken = default);

        /// <summary>
        /// Update secret
        /// Update an existing secret. After updating, the version is automatically incremented.
        /// Only value and entity_id can be updated.
        /// </summary>
        Task<SecretResponse> UpdateSecret(string name, SecretRequest secretRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete secret
        /// Permanently delete a secret by name.
        /// </summary>
        Task<EmptyResponse> DeleteSecret(string name, CancellationToken cancellationToken = default);
    }
}