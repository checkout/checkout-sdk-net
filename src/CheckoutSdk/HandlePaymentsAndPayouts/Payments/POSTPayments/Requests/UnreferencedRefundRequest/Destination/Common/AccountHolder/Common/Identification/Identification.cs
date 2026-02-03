namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.Identification
{
    /// <summary>
    /// identification
    /// The account holder's identification.
    /// Providing identification details for the unreferenced refund recipient increases the likelihood of a successful
    /// unreferenced refund.
    /// </summary>
    public class Identification
    {
        /// <summary>
        /// The type of identification for the account holder.
        /// [Required]
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The account holder's identification number.
        /// [Required]
        /// &lt;= 25
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// If applicable, the country that issued the account holder's identification, as a two-letter ISO country
        /// code.
        /// Providing issuing_country increases the likelihood of a successful identity verification.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string IssuingCountry { get; set; }

        /// <summary>
        /// If applicable, the expiration date of the account holder's identification, in the format YYYY-MM-DD.
        /// Providing date_of_expiry increases the likelihood of a successful identity verification.
        /// [Optional]
        /// 10 characters
        /// </summary>
        public string DateOfExpiry { get; set; }
    }
}