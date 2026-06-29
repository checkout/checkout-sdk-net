namespace Checkout.Payments.Request
{
    public class PaymentInstruction
    {
        /// <summary>
        /// The purpose of the payment. May be required for AFT transactions depending on the card's issuer_country.
        /// [Optional]
        /// Enum: "donations" "education" "emergency_need" "expatriation" "family_support" "financial_services"
        ///       "gifts" "income" "insurance" "investment" "it_services" "leisure" "loan_payment"
        ///       "medical_treatment" "other" "pension" "royalties" "savings" "travel_and_tourism"
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Specifies who bears the transaction charge (used in bank/SWIFT transfers).
        /// [Optional]
        /// </summary>
        public string ChargeBearer { get; set; }

        /// <summary>
        /// Enables repair mode for failed transactions.
        /// [Optional]
        /// </summary>
        public bool? Repair { get; set; }

        /// <summary>
        /// Restricts the payout to the specified payment scheme type. When not available, payouts will fail.
        /// [Optional]
        /// Enum: "swift" "local" "instant"
        /// </summary>
        public InstructionScheme? Scheme { get; set; }

        /// <summary>
        /// The identifier of a forex quote to apply to this instruction.
        /// [Optional]
        /// </summary>
        public string QuoteId { get; set; }

        /// <summary>
        /// When true, skips the expiry check on the instruction.
        /// [Optional]
        /// </summary>
        public bool? SkipExpiry { get; set; }

        /// <summary>
        /// The funds transfer type code for the type of payout being performed.
        /// If only one code has been assigned, this field is optional.
        /// [Optional]
        /// </summary>
        public string FundsTransferType { get; set; }

        /// <summary>
        /// The Mastercard Vendor Value (MVV) for the payout.
        /// [Optional]
        /// </summary>
        public string Mvv { get; set; }
    }
}