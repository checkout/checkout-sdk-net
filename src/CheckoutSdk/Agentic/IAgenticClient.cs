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