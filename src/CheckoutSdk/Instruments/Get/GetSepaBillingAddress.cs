using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The billing address of the account holder of a stored SEPA instrument.
    /// </summary>
    public class GetSepaBillingAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Required]
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// [Required]
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The address city.
        /// [Required]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address zip/postal code.
        /// [Required]
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the address.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
