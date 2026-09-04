using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The details of a stored bank account instrument.
    /// </summary>
    public class GetBankAccountInstrumentResponse : GetInstrumentResponse
    {
        public GetBankAccountInstrumentResponse() : base(InstrumentType.BankAccount)
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
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The two-letter ISO country code of where the account is based.
        /// [Required]
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The bank details.
        /// [Optional]
        /// </summary>
        public BankDetails Bank { get; set; }
    }
}
