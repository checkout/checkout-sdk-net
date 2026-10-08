using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's address from their Cash App profile. The keys follow Cash App's naming
    /// (address_line_1, locality, administrative_district_level_1), not the Checkout.com address, so
    /// the line and district properties carry an explicit name: the snake case naming strategy alone
    /// would produce address_line1.
    /// </summary>
    public class CashAppAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Optional]
        /// readOnly
        /// </summary>
        [JsonProperty(PropertyName = "address_line_1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// [Optional]
        /// readOnly
        /// </summary>
        [JsonProperty(PropertyName = "address_line_2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// [Optional]
        /// readOnly
        /// </summary>
        [JsonProperty(PropertyName = "address_line_3")]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// The address locality, such as the city or town.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// The address sublocality, such as the district or neighborhood.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Sublocality { get; set; }

        /// <summary>
        /// The address's top-level administrative district, such as the state or province.
        /// [Optional]
        /// readOnly
        /// </summary>
        [JsonProperty(PropertyName = "administrative_district_level_1")]
        public string AdministrativeDistrictLevel1 { get; set; }

        /// <summary>
        /// The postal or zip code.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The address country, in ISO 3166-1 alpha-2 format.
        /// [Optional]
        /// readOnly
        /// max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
