using System.Threading;
using System.Threading.Tasks;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic API Client
    /// </summary>
    public interface IAgenticClient
    {
        /// <summary>
        /// Enroll in agentic services
        /// Enrolls a merchant or entity in agentic services
        /// </summary>
        Task<AgenticEnrollResponse> Enroll(
            AgenticEnrollRequest agenticEnrollRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a purchase intent
        /// Creates a new purchase intent for agentic commerce
        /// </summary>
        Task<AgenticPurchaseIntentResponse> CreatePurchaseIntent(
            AgenticPurchaseIntentCreateRequest agenticPurchaseIntentCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a purchase intent credentials
        /// Create credentials for an agentic commerce purchase intent.
        /// </summary>
        Task<AgenticPurchaseIntentResponse> CreatePurchaseIntentCredentials(string id,
            AgenticPurchaseIntentCredentialsCreateRequest agenticPurchaseIntentCredentialsCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a purchase intent
        /// Updates a purchase intent for agentic commerce
        /// </summary>
        Task<AgenticPurchaseIntentResponse> UpdatePurchaseIntent(string id,
            AgenticPurchaseIntentUpdateRequest agenticPurchaseIntentUpdateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a purchase intent
        /// Deletes a purchase intent for agentic commerce
        /// </summary>
        Task<EmptyResponse> DeletePurchaseIntent(string id,
            CancellationToken cancellationToken = default);
    }
}