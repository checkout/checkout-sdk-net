using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The ACH payment method's details and configuration.
    /// </summary>
    public class Ach : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The type of Direct Debit account.
        /// [Optional]
        /// </summary>
        public PaymentSetupAchAccountType? AccountType { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public AchAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The account number of the Direct Debit account.
        /// [Optional] writeOnly
        /// min 4 characters, max 17 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The bank code of the Direct Debit account.
        /// [Optional] writeOnly
        /// min 8 characters, max 9 characters
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the bank account.
        /// [Optional]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
