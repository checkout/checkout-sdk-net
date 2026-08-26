using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    /// <summary>
    /// The client for the instruments API.
    /// </summary>
    public interface IInstrumentsClient
    {
        /// <summary>
        /// Store a payment instrument. Calls POST /instruments.
        /// The concrete request type selects the instrument type: bank_account, card, token, sepa,
        /// ach or bacs.
        /// </summary>
        /// <param name="createInstrumentRequest">The instrument to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<CreateInstrumentResponse> Create(CreateInstrumentRequest createInstrumentRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the details of a stored payment instrument. Calls GET /instruments/{id}.
        /// The returned object is the concrete variant matching the instrument's type.
        /// </summary>
        /// <param name="instrumentId">The payment instrument ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<GetInstrumentResponse> Get(string instrumentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the details of a stored payment instrument. Calls PATCH /instruments/{id}.
        /// </summary>
        /// <param name="instrumentId">The payment instrument ID.</param>
        /// <param name="updateInstrumentRequest">The details to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<UpdateInstrumentResponse> Update(string instrumentId,
            UpdateInstrumentRequest updateInstrumentRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a stored payment instrument. Calls DELETE /instruments/{id}.
        /// Returns no content on success.
        /// </summary>
        /// <param name="instrumentId">The payment instrument ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<EmptyResponse> Delete(string instrumentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke a payment instrument. The instrument status is set to INVALID with the reason
        /// revoked_by_merchant. The instrument record is retained for audit purposes.
        /// Calls PATCH /instruments/{id}/revoke, which is no longer declared in the API
        /// specification.
        /// </summary>
        /// <param name="instrumentId">The payment instrument ID. Pattern: ^(src_)[a-z0-9]{26}$.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<EmptyResponse> Revoke(string instrumentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the bank account field formatting requirements for a country and currency.
        /// Calls GET /validation/bank-accounts/{country}/{currency}. Requires OAuth.
        /// </summary>
        /// <param name="country">The two-letter ISO country code of the account.</param>
        /// <param name="currency">The three-letter ISO currency code of the account.</param>
        /// <param name="bankAccountFieldQuery">Optional filters on account holder type and payment network.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<BankAccountFieldResponse> GetBankAccountFieldFormatting(CountryCode country, Currency currency,
            BankAccountFieldQuery bankAccountFieldQuery, CancellationToken cancellationToken = default);
    }
}
