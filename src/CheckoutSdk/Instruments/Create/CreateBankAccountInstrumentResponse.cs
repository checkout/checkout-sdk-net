using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store bank account instrument response.
    /// The type, id and fingerprint are inherited from CreateInstrumentResponse.
    /// </summary>
    public class CreateBankAccountInstrumentResponse : CreateInstrumentResponse
    {
        public CreateBankAccountInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// The details of the bank that holds the account.
        /// [Optional]
        /// </summary>
        public BankDetails Bank { get; set; }

        /// <summary>
        /// The 8 or 11 character code which identifies the bank or bank branch.
        /// [Optional]
        /// </summary>
        public string SwiftBic { get; set; }

        /// <summary>
        /// The number, which can contain letters, that identifies the account.
        /// [Optional]
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The code that identifies the bank.
        /// [Optional]
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The internationally agreed standard for identifying a bank account.
        /// [Optional]
        /// </summary>
        public string Iban { get; set; }
    }
}
