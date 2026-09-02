using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// An ach payment source.
    /// </summary>
    public class RequestAchSource : AbstractRequestSource
    {
        /// <summary>
        /// The type of Direct Debit account.
        /// [Required]
        /// Enum: "savings" "checking" "cash"
        /// This is deliberately not Checkout.Common.AccountType, which declares
        /// "current" instead of "checking" and is rejected at this position.
        /// </summary>
        public AchSourceAccountType? AccountType { get; set; }

        /// <summary>
        /// The source country.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The account number of the Direct Debit account.
        /// [Required]
        /// min 4 characters, max 17 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The bank code of the Direct Debit account.
        /// [Required]
        /// min 8 characters, max 9 characters
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The account holder's details. Maps swagger AccountHolderAch, which declares
        /// only type, first_name, last_name, company_name, date_of_birth,
        /// billing_address and identification.
        /// [Required]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        public RequestAchSource() : base(PaymentSourceType.Ach)
        {
        }
    }
}
