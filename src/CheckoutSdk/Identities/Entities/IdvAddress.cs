using Checkout.Common;
namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The applicant's address.
    /// </summary>
    public class IdvAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Optional]
        /// max 250 characters
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// [Optional]
        /// max 250 characters
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The city or town.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state, county, or province.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The postal or ZIP code.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the address.
        /// [Optional]
        /// Standard: ISO 3166-1 alpha-2 country code
        /// max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
