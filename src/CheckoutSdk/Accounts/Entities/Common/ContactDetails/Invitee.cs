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
        /// [Required] in the hosted onboarding invite request (which also requires <c>reference</c> and
        /// <c>is_draft</c>); [Optional] in the full onboarding requests (every Full and Lite variant);
        /// not part of the US ISV Seller variants.
        /// Format: email
        /// </summary>
        public string Email { get; set; }
    }
}