namespace Checkout.Accounts.Entities.Common.ContactDetails
{
    /// <summary>
    /// The details of the user responsible for onboarding the sub-entity.
    /// </summary>
    public class Invitee
    {
        /// <summary>
        /// The main email address for this sub-entity. Despite the spec's wording, this is the address
        /// of the invitee, the user responsible for onboarding the sub-entity.
        /// [Optional]
        /// Format: email
        /// </summary>
        public string Email { get; set; }
    }
}