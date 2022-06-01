using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    public interface IInstrumentsClient
    {
        Task<RetrieveInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default);

        Task<CreateInstrumentResponse> Create(CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<UpdateInstrumentResponse> Update(string instrumentId, UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> Delete(string instrumentId, CancellationToken cancellationToken = default);
    }
}