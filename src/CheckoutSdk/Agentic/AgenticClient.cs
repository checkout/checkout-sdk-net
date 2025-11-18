using System.Threading;
using System.Threading.Tasks;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic API Client
    /// </summary>
    public class AgenticClient : AbstractClient, IAgenticClient
    {
        private const string AgenticPath = "agentic";
        private const string EnrollPath = "enroll";
        private const string PurchaseIntentPath = "purchase-intent";
        private const string CredentialsPath = "credentials";

        public AgenticClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        /// <summary>
        /// Enroll in agentic services
        /// Enrolls a card for use with agentic commerce.
        /// </summary>
        public Task<AgenticEnrollResponse> Enroll(
            AgenticEnrollRequest agenticEnrollRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticEnrollRequest", agenticEnrollRequest);
            return ApiClient.Post<AgenticEnrollResponse>(
                BuildPath(AgenticPath, EnrollPath),
                SdkAuthorization(),
                agenticEnrollRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Create a purchase intent
        /// Creates a new purchase intent for agentic commerce
        /// </summary>
        public Task<AgenticPurchaseIntentResponse> CreatePurchaseIntent(
            AgenticPurchaseIntentCreateRequest agenticPurchaseIntentCreateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticPurchaseIntentCreateRequest", agenticPurchaseIntentCreateRequest);
            return ApiClient.Post<AgenticPurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath),
                SdkAuthorization(),
                agenticPurchaseIntentCreateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Update a purchase intent
        /// Updates a new purchase intent for agentic commerce
        /// </summary>
        public Task<AgenticPurchaseIntentResponse> UpdatePurchaseIntent(string id,
            AgenticPurchaseIntentUpdateRequest agenticPurchaseIntentUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id, "agenticPurchaseIntentUpdateRequest", agenticPurchaseIntentUpdateRequest);
            return ApiClient.Put<AgenticPurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath, id),
                SdkAuthorization(),
                agenticPurchaseIntentUpdateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Delete a purchase intent
        /// Deletes a purchase intent for agentic commerce
        /// </summary>
        public Task<EmptyResponse> DeletePurchaseIntent(string id,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id);
            return ApiClient.Delete<EmptyResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath, id),
                SdkAuthorization(),
                cancellationToken
            );
        }
    }
}