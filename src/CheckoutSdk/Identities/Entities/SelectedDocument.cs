using Checkout.Common;

namespace Checkout.Identities.Entities
{
    public class SelectedDocument
    {
        /// <summary>
        /// The country that issued the selected document.
        /// [Optional]
        /// Standard: ISO 3166-1 alpha-2 country code
        /// Pattern: ^[A-Za-z]{2}$
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The type of identity document
        /// </summary>
        public DocumentType DocumentType { get; set; }
    }
}