using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    public interface IInstrumentsClient
    {
        Task<CreateInstrumentResponse> Create(CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<GetInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default);

        Task<UpdateInstrumentResponse> Update(string instrumentId,
            UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> Delete(string instrumentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke a payment instrument. The instrument status is set to INVALID with the reason
        /// revoked_by_merchant. The instrument record is retained for audit purposes.
        /// </summary>
        /// <param name="instrumentId">The payment instrument ID. Pattern: ^(src_)[a-z0-9]{26}$.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<EmptyResponse> Revoke(string instrumentId, CancellationToken cancellationToken = default);

        Task<BankAccountFieldResponse> GetBankAccountFieldFormatting(CountryCode country, Currency currency,
            BankAccountFieldQuery bankAccountFieldQuery, CancellationToken cancellationToken = default);
    }
}