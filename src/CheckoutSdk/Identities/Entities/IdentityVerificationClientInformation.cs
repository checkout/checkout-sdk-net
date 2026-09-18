using Checkout.Common;
namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The applicant's details for an identity verification attempt.
    /// </summary>
    public class IdentityVerificationClientInformation : ClientInformation
    {
        /// <summary>
        /// The country that issued the applicant's identity document.
        /// [Optional]
        /// Standard: ISO 3166-1 alpha-2 country code
        /// Pattern: ^[A-Z]{2}
        /// Example: FR
        /// </summary>
        public CountryCode? PreSelectedDocumentIssuingCountry { get; set; }

        /// <summary>
        /// The type of identity document the applicant uses for the attempt.
        /// [Optional]
        /// </summary>
        public DocumentType? PreSelectedDocumentType { get; set; }
    }
}
