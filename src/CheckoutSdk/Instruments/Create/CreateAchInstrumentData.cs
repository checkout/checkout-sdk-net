using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The details of the ACH account being stored.
    /// </summary>
    public class CreateAchInstrumentData
    {
        /// <summary>
        /// The type of bank account.
        /// [Required]
        /// </summary>
        public AchAccountType? AccountType { get; set; }

        /// <summary>
        /// The account number of the ACH account.
        /// [Required]
        /// min 4 characters, max 17 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The routing number of the ACH account.
        /// [Required]
        /// min 8 characters, max 9 characters
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The currency of the account.
        /// [Required]
        /// min 3 characters, max 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The country of the account, as an ISO 3166-1 alpha-2 code.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
