using System.Threading;
using System.Threading.Tasks;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;

namespace Checkout.Agentic
{
    /// <summary>
    /// Agentic Commerce Client Interface
    /// </summary>
    public interface IAgenticClient
    {
        /// <summary>
        /// Create a new agentic commerce
        /// Creates a new autonomous commerce agent with specified configuration and capabilities.
        /// </summary>
        /// <param name="createRequest">The create agentic commerce request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The create agentic commerce response</returns>
        Task<CreateAgenticCommerceResponse> CreateAgenticCommerce(
            CreateAgenticCommerceRequest createRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get agentic commerce details
        /// Retrieves detailed information about a specific agentic commerce agent,
        /// including configuration, statistics, and current status.
        /// </summary>
        /// <param name="agenticCommerceId">The unique identifier of the agentic commerce</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The get agentic commerce response</returns>
        Task<GetAgenticCommerceResponse> GetAgenticCommerce(
            string agenticCommerceId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update agentic commerce
        /// Updates the configuration, settings, or metadata of an existing agentic commerce agent.
        /// </summary>
        /// <param name="agenticCommerceId">The unique identifier of the agentic commerce</param>
        /// <param name="updateRequest">The update agentic commerce request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The update agentic commerce response</returns>
        Task<UpdateAgenticCommerceResponse> UpdateAgenticCommerce(
            string agenticCommerceId,
            UpdateAgenticCommerceRequest updateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete agentic commerce
        /// Permanently removes an agentic commerce agent and all its associated data.
        /// This action cannot be undone.
        /// </summary>
        /// <param name="agenticCommerceId">The unique identifier of the agentic commerce</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The delete agentic commerce response</returns>
        Task<DeleteAgenticCommerceResponse> DeleteAgenticCommerce(
            string agenticCommerceId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// List agentic commerce agents
        /// Retrieves a paginated list of agentic commerce agents with optional filtering and sorting.
        /// </summary>
        /// <param name="listRequest">The list agentic commerce request with filtering and pagination options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The list agentic commerce response</returns>
        Task<ListAgenticCommerceResponse> ListAgenticCommerce(
            ListAgenticCommerceRequest listRequest = null,
            CancellationToken cancellationToken = default);
    }
}