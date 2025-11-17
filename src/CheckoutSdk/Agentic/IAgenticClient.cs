using System.Threading;
using System.Threading.Tasks;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic Client Interface
    /// </summary>
    public interface IAgenticClient
    {
        /// <summary>
        /// Create a new agentic
        /// Creates a new autonomous agent with specified configuration and capabilities.
        /// </summary>
        /// <param name="createRequest">The create agentic request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The create agentic response</returns>
        Task<CreateAgenticResponse> CreateAgentic(
            CreateAgenticRequest createRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get agentic details
        /// Retrieves detailed information about a specific agentic agent,
        /// including configuration, statistics, and current status.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The get agentic response</returns>
        Task<GetAgenticResponse> GetAgentic(
            string agenticId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update agentic
        /// Updates the configuration, settings, or metadata of an existing agentic agent.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="updateRequest">The update agentic request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The update agentic response</returns>
        Task<UpdateAgenticResponse> UpdateAgentic(
            string agenticId,
            UpdateAgenticRequest updateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete agentic
        /// Permanently removes an agentic agent and all its associated data.
        /// This action cannot be undone.
        /// </summary>
        /// <param name="agenticId">The unique identifier of the agentic</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The delete agentic response</returns>
        Task<DeleteAgenticResponse> DeleteAgentic(
            string agenticId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get agentics
        /// Retrieves a paginated list of agentic agents with optional filtering and sorting.
        /// </summary>
        /// <param name="getAgenticsRequest">The request with filtering and pagination options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The get agentics response</returns>
        Task<GetAgenticsResponse> GetAgentics(
            GetAgenticsRequest getAgenticsRequest = null,
            CancellationToken cancellationToken = default);
    }
}