using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The billing address of the account holder of a Bacs Direct Debit instrument being stored.
    /// </summary>
    public class CreateBacsBillingAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Optional]
        /// max 200 characters
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The street number. If no number, pass "w/n".
        /// [Optional]
        /// max 10 characters
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The address city.
        /// [Optional]
        /// max 35 characters
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address zip/postal code.
        /// [Optional]
        /// max 16 characters
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
