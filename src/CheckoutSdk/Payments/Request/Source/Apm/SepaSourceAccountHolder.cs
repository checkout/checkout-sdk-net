namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The account holder's personal information on a SEPA payment source.
    /// Maps the account_holder object of PaymentRequestSEPAV4Source. Deliberately not
    /// Checkout.Common.AccountHolder, which is a superset carrying a phone, an identification, a
    /// date of birth and a tax ID that this position does not declare. The property names match
    /// Checkout.Instruments.Create.CreateSepaAccountHolder, but the positions differ: only the
    /// billing address is required here, where the instrument requires the names too.
    /// </summary>
    public class SepaSourceAccountHolder
    {
        /// <summary>
        /// The account holder's billing address.
        /// [Required]
        /// </summary>
        public SepaSourceBillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The account holder's first name.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The account holder's company name.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public SepaSourceAccountHolderType? Type { get; set; }
    }
}
