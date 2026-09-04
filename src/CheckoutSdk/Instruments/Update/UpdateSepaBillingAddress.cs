using Checkout.Common;

namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The billing address of the account holder of a SEPA instrument being updated.
    /// </summary>
    public class UpdateSepaBillingAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Required]
        /// max 200 characters
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The street number. If no number, pass "w/n".
        /// [Required]
        /// max 10 characters
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The address city.
        /// [Required]
        /// max 50 characters
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address zip/postal code.
        /// [Required]
        /// max 50 characters
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
