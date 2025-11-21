using System.Threading;
using System.Threading.Tasks;
using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic Commerce API Client
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
        
        // Agentic Commerce enrollment
        // ----------------------------------------------------------------

        /// <summary>
        /// Enroll a card for use with agentic commerce
        /// [BETA]
        /// </summary>
        public Task<EnrollACardResponse> Enroll(
            EnrollACardRequest enrollACardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticEnrollRequest", enrollACardRequest);
            return ApiClient.Post<EnrollACardResponse>(
                BuildPath(AgenticPath, EnrollPath),
                SdkAuthorization(),
                enrollACardRequest,
                cancellationToken
            );
        }
        
        // Purchase intents
        // ----------------------------------------------------------------

        /// <summary>
        /// Create an agentic commerce purchase intent
        /// [BETA]
        /// </summary>
        public Task<PurchaseIntentResponse> CreatePurchaseIntent(
            PurchaseIntentCreateRequest purchaseIntentCreateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticPurchaseIntentCreateRequest", purchaseIntentCreateRequest);
            return ApiClient.Post<PurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath),
                SdkAuthorization(),
                purchaseIntentCreateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Create credentials for an agentic commerce purchase intent.
        /// [BETA]
        /// </summary>
        public Task<PurchaseIntentResponse> CreatePurchaseIntentCredentials(string id,
            PurchaseIntentCredentialsCreateRequest purchaseIntentCredentialsCreateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id,"agenticPurchaseIntentCredentialsCreateRequest", 
            purchaseIntentCredentialsCreateRequest);
            return ApiClient.Post<PurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath, id, CredentialsPath),
                SdkAuthorization(),
                purchaseIntentCredentialsCreateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Update an agentic commerce purchase intent
        /// [BETA]
        /// </summary>
        public Task<PurchaseIntentResponse> UpdatePurchaseIntent(string id,
            PurchaseIntentUpdateRequest purchaseIntentUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id, "agenticPurchaseIntentUpdateRequest", purchaseIntentUpdateRequest);
            return ApiClient.Put<PurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath, id),
                SdkAuthorization(),
                purchaseIntentUpdateRequest,
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