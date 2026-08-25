using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The billing address of the account holder of a stored Bacs Direct Debit instrument.
    /// </summary>
    public class GetBacsBillingAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Optional]
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// [Optional]
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The address city.
        /// [Optional]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address ZIP or postal code.
        /// [Optional]
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The address country.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
