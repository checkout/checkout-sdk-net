namespace Checkout.Accounts.Entities.Common.ContactDetails
{
    /// <summary>
    /// Email addresses for this sub-entity.
    /// </summary>
    public class EmailAddresses
    {
        /// <summary>
        /// The main email address for this sub-entity.
        /// [Required]
        /// Format: email
        /// </summary>
        public string Primary { get; set; }
    }
}