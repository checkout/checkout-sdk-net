using Checkout.Common;

namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Updates the details of a stored bank account instrument.
    /// </summary>
    public class UpdateBankInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateBankInstrumentRequest() : base(InstrumentType.BankAccount)
        {
        }

        /// <summary>
        /// The type of account.
        /// [Optional]
        /// </summary>
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// Number (which can contain letters) that identifies the account.
        /// [Optional]
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Code that identifies the bank.
        /// [Optional]
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// Code that identifies the bank branch.
        /// [Optional]
        /// </summary>
        public string BranchCode { get; set; }

        /// <summary>
        /// Internationally agreed standard for identifying bank account.
        /// [Optional]
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// The combination of bank code and/or branch code and account number.
        /// [Optional]
        /// </summary>
        public string Bban { get; set; }

        /// <summary>
        /// 8 or 11 character code which identifies the bank or bank branch.
        /// [Optional]
        /// </summary>
        public string SwiftBic { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the account's currency.
        /// [Optional]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The two-letter ISO country code of where the account is based.
        /// [Optional]
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The ID of the primary processing channel this instrument is intended to be used for.
        /// [Optional]
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The bank account holder details. Having accurate and complete data improves payout
        /// performance: it increases the success rate and prevents delays.
        /// [Optional]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Not aligned with the API specification. The specification declares this field as bank,
        /// but this property serializes as bank_details, which the API does not accept. There is no
        /// property on this class that serializes as bank, so the bank details cannot currently be
        /// sent on a bank account instrument update.
        /// </summary>
        public BankDetails BankDetails { get; set; }

        /// <summary>
        /// The customer details.
        /// [Optional]
        /// </summary>
        public UpdateCustomerRequest Customer { get; set; }
    }
}
