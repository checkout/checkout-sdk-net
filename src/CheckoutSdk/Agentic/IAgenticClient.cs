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
    }
}