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
        public Task<AgenticCreatePurchaseIntentResponse> CreatePurchaseIntent(
            AgenticCreatePurchaseIntentRequest agenticCreatePurchaseIntentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticCreatePurchaseIntentRequest", agenticCreatePurchaseIntentRequest);
            return ApiClient.Post<AgenticCreatePurchaseIntentResponse>(
                BuildPath(AgenticPath, PurchaseIntentPath),
                SdkAuthorization(),
                agenticCreatePurchaseIntentRequest,
                cancellationToken
            );
        }
    }
}