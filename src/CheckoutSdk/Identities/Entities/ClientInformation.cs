using Checkout.Common;
namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The applicant's details.
    /// </summary>
    public class ClientInformation
    {
        /// <summary>
        /// The applicant's residence country.
        /// [Optional]
        /// Standard: ISO 3166-1 alpha-2 country code
        /// Pattern: ^[A-Z]{2}
        /// Example: FR
        /// </summary>
        public CountryCode? PreSelectedResidenceCountry { get; set; }

        /// <summary>
        /// The language you want to use for the user interface.
        /// [Optional]
        /// Format: IETF BCP 47 language tag
        /// Example: en-US
        /// </summary>
        public string PreSelectedLanguage { get; set; }
    }
}
