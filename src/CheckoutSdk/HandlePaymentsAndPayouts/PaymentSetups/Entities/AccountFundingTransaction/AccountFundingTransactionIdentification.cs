namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Sender identification details.
    /// </summary>
    public class AccountFundingTransactionIdentification
    {
        /// <summary>
        /// The type of identification used to identify the sender.
        /// [Optional]
        /// Enum: "passport" "driving_license" "national_id"
        /// </summary>
        public AccountFundingTransactionIdentificationType? Type { get; set; }

        /// <summary>
        /// The identification number.
        /// [Optional]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the country that issued the identification.
        /// [Optional]
        /// </summary>
        public string IssuingCountry { get; set; }
    }
}
