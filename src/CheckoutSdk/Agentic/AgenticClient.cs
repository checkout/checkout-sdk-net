using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic API Client
    /// </summary>
    public class AgenticClient : AbstractClient, IAgenticClient
    {
        private const string AgenticPath = "agentic";
        private const string EnrollPath = "enroll";

        public AgenticClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
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
    }
}