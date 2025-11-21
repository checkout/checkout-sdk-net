using System.Threading;
using System.Threading.Tasks;
using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic Commerce API Client
    /// </summary>
    public interface IAgenticClient
    {
        // Agentic Commerce enrollment
        // ----------------------------------------------------------------
        
        /// <summary>
        /// Enroll a card for use with agentic commerce
        /// [BETA]
        /// </summary>
        Task<EnrollACardResponse> Enroll(
            EnrollACardRequest enrollACardRequest,
            CancellationToken cancellationToken = default);
        
        // Purchase intents
        // ----------------------------------------------------------------

        /// <summary>
        /// Create an agentic commerce purchase intent
        /// [BETA]
        /// </summary>
        Task<PurchaseIntentResponse> CreatePurchaseIntent(
            PurchaseIntentCreateRequest purchaseIntentCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create credentials for an agentic commerce purchase intent.
        /// [BETA]
        /// </summary>
        Task<PurchaseIntentResponse> CreatePurchaseIntentCredentials(string id,
            PurchaseIntentCredentialsCreateRequest purchaseIntentCredentialsCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an agentic commerce purchase intent
        /// [BETA]
        /// </summary>
        Task<PurchaseIntentResponse> UpdatePurchaseIntent(string id,
            PurchaseIntentUpdateRequest purchaseIntentUpdateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an agentic commerce purchase intent
        /// [BETA]
        /// </summary>
        Task<EmptyResponse> DeletePurchaseIntent(string id,
            CancellationToken cancellationToken = default);
    }
}