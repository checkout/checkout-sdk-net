using System.Collections.Generic;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The result of the address document check.
    /// </summary>
    public class AddressDocumentResult
    {
        /// <summary>
        /// The type of address document submitted.
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// The issuer of the address document.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The full names of the people named on the document.
        /// </summary>
        public List<string> FullNames { get; set; }

        /// <summary>
        /// The date the document was issued. (Format: date, yyyy-MM-dd)
        /// </summary>
        public string IssueDate { get; set; }

        /// <summary>
        /// The address extracted from the document.
        /// </summary>
        public AdvAddress Address { get; set; }
    }
}
