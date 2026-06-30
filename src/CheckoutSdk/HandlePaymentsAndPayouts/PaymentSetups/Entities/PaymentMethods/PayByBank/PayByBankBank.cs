namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// A bank available for the customer to select for a Pay by Bank payment.
    /// </summary>
    public class PayByBankBank
    {
        /// <summary>
        /// The unique identifier of the bank.
        /// [Optional]
        /// </summary>
        public string BankId { get; set; }

        /// <summary>
        /// The display name of the bank.
        /// [Optional]
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The URL of the bank's logo.
        /// [Optional]
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Whether the bank is currently available for selection.
        /// [Optional]
        /// </summary>
        public bool? Available { get; set; }
    }
}
