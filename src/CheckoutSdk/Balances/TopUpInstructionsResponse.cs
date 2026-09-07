using Checkout.Common;

namespace Checkout.Balances
{
    /// <summary>
    /// The bank details and payment reference used to top up a sub-account.
    /// </summary>
    public class TopUpInstructionsResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the sub-account that the instructions apply to.
        /// [Required]
        /// </summary>
        public string CurrencyAccountId { get; set; }

        /// <summary>
        /// The currency that funds must be sent in, as a three-letter ISO 4217 currency code.
        /// This is the sub-account's holding currency, returned as holding_currency by the
        /// Retrieve entity balances endpoint.
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The reference that must be quoted on the payment. It is how an incoming payment is
        /// attributed to the sub-account. A payment sent without this reference may not be
        /// credited.
        /// [Required]
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// The bank details for each available funding rail.
        /// [Required]
        /// </summary>
        public TopUpBankDetails BankDetails { get; set; }
    }
}
