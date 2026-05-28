namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Account funding transaction details for the payment.
    /// </summary>
    public class PaymentSetupAccountFundingTransaction
    {
        /// <summary>
        /// Whether to process this payment as an account funding transaction.
        /// [Optional]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Specifies the purpose of the account funding transaction.
        /// [Optional]
        /// Enum: "donations" "education" "emergency_need" "expatriation" "family_support"
        /// "financial_services" "gifts" "income" "insurance" "investment" "it_services"
        /// "leisure" "loan_payment" "medical_treatment" "other" "pension" "royalties"
        /// "savings" "travel_and_tourism"
        /// </summary>
        public AccountFundingTransactionPurpose? Purpose { get; set; }

        /// <summary>
        /// Account funding transaction sender details.
        /// [Optional]
        /// </summary>
        public AccountFundingTransactionSender Sender { get; set; }

        /// <summary>
        /// Account funding transaction recipient details.
        /// [Optional]
        /// </summary>
        public AccountFundingTransactionRecipient Recipient { get; set; }
    }
}
