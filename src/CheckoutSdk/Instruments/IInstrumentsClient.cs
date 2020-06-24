using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    /// <summary>
    /// Defines the operations available on the Checkout.com Instruments API.
    /// </summary>
    public interface IInstrumentsClient
    {
        /// <summary>
        /// Create a payment instrument that you can later use as the source or destination for one or more payments.
        /// </summary>
        /// <param name="instrumentRequest">The payment instrument details such as type and token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        /// <returns>A task that upon completion contains the <see cref="InstrumentResponse"/>.</returns>
        Task<InstrumentResponse> CreateAsync(InstrumentRequest instrumentRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
