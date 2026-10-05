namespace Checkout.Accounts.Entities.Common.ContactDetails
{
    /// <summary>
    /// Email addresses for this sub-entity.
    /// </summary>
    public class EmailAddresses
    {
        /// <summary>
        /// The main email address for this sub-entity.
        /// [Required] in every variant that sends <c>email_addresses</c>.
        /// Format: email
        /// </summary>
        public string Primary { get; set; }

        /// <summary>
        /// The email address of the person responsible for PCI compliance at this sub-entity.
        /// [Required] for the US ISV Seller variants (3.0), together with Primary; not part of the other variants.
        /// Format: email
        /// </summary>
        public string PciComplianceContact { get; set; }
    }
}
