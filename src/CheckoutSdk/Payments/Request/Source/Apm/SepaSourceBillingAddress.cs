using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The account holder's billing address on a SEPA payment source.
    /// Every property is required. Deliberately not Checkout.Common.Address, which also declares a
    /// State that this position does not accept.
    /// </summary>
    public class SepaSourceBillingAddress
    {
        /// <summary>
        /// The account holder's street name.
        /// [Required]
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The account holder's street number.
        /// [Required]
        /// max 10 characters
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The account holder's city.
        /// [Required]
        /// max 35 characters
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The account holder's zip code.
        /// [Required]
        /// max 16 characters
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The account holder's country, as an ISO 3166-1 alpha-2 code.
        /// [Required]
        /// max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
