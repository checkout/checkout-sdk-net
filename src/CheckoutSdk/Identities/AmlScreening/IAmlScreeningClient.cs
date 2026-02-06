using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AmlScreening.Requests;
using Checkout.Identities.AmlScreening.Responses;

namespace Checkout.Identities.AmlScreening
{
    /// <summary>
    ///     Client for managing AML screenings in identity verification processes
    /// </summary>
    public interface IAmlScreeningClient
    {
        /// <summary>
        ///     Creates a new AML screening
        /// </summary>
        /// <param name="amlScreeningRequest">the AML screening request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the AML screening response</returns>
        Task<AmlScreeningResponse> CreateAmlScreening(AmlScreeningRequest amlScreeningRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves an existing AML screening by ID
        /// </summary>
        /// <param name="screeningId">the screening ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the AML screening response</returns>
        Task<AmlScreeningResponse> GetAmlScreening(string screeningId, CancellationToken cancellationToken = default);
    }
}