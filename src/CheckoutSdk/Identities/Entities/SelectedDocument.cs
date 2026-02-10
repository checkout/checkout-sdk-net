using Checkout.Common;

namespace Checkout.Identities.Entities
{
    public class SelectedDocument
    {
        /// <summary>
        /// The applicant's nationality. Standard – ISO alpha-2 country code
        /// </summary>
        public CountryCode Country { get; set; }

        /// <summary>
        /// The type of identity document
        /// </summary>
        public DocumentType DocumentType { get; set; }
    }
}