using System.Threading;
using System.Threading.Tasks;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic Client
    /// Provides functionality to manage autonomous agents and their operations.
    /// </summary>
    public class AgenticClient : AbstractClient, IAgenticClient
    {
        private const string AgenticPath = "agentic";

        /// <summary>
        /// Initializes a new instance of the AgenticClient
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="configuration">The checkout configuration</param>
        public AgenticClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Create a new agentic
        /// Creates a new autonomous agent with specified configuration and capabilities.
        /// </summary>
        /// <param name="createRequest">The create agentic request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The create agentic response</returns>
        public Task<CreateAgenticResponse> CreateAgentic(
            CreateAgenticRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createRequest", createRequest);
            return ApiClient.Post<CreateAgenticResponse>(
                BuildPath(AgenticPath),
                SdkAuthorization(),
                createRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Get agentic details
        /// Retrieves detailed information about a specific agentic agent,
        /// including configuration, statistics, and current status.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The get agentic response</returns>
        public Task<GetAgenticResponse> GetAgentic(
            string agenticId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticId", agenticId);
            return ApiClient.Get<GetAgenticResponse>(
                BuildPath(AgenticPath, agenticId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        /// <summary>
        /// Update agentic
        /// Updates the configuration, settings, or metadata of an existing agentic agent.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="updateRequest">The update agentic request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The update agentic response</returns>
        public Task<UpdateAgenticResponse> UpdateAgentic(
            string agenticId,
            UpdateAgenticRequest updateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticId", agenticId);
            CheckoutUtils.ValidateParams("updateRequest", updateRequest);
            return ApiClient.Patch<UpdateAgenticResponse>(
                BuildPath(AgenticPath, agenticId),
                SdkAuthorization(),
                updateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Delete agentic
        /// Permanently removes an agentic agent and all its associated data.
        /// This action cannot be undone.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The delete agentic response</returns>
        public Task<DeleteAgenticResponse> DeleteAgentic(
            string agenticId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("agenticId", agenticId);
            return ApiClient.Delete<DeleteAgenticResponse>(
                BuildPath(AgenticPath, agenticId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        /// <summary>
        /// Get agentics
        /// Retrieves a paginated list of agentic agents with optional filtering and sorting.
        /// </summary>
        /// <param name="getAgenticsRequest">The request with filtering and pagination options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The get agentics response</returns>
        public Task<GetAgenticsResponse> GetAgentics(
            GetAgenticsRequest getAgenticsRequest = null,
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Query<GetAgenticsResponse>(
                BuildPath(AgenticPath),
                SdkAuthorization(),
                getAgenticsRequest,
                cancellationToken
            );
        }
    }
}