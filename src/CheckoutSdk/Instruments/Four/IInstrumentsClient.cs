using Checkout.Common;
using Checkout.Instruments.Four.Get;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments.Four
{
    public interface IInstrumentsClient
    {
        Task<Create.CreateInstrumentResponse> Create(Create.CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<GetInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default);

        Task<Update.UpdateInstrumentResponse> Update(string instrumentId,
            Update.UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<object> Delete(string instrumentId, CancellationToken cancellationToken = default);

        Task<BankAccountFieldResponse> GetBankAccountFieldFormatting(CountryCode country, Currency currency,
            BankAccountFieldQuery bankAccountFieldQuery, CancellationToken cancellationToken = default);
    }
}