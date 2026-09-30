using Checkout.Common;

namespace Checkout.Accounts.Entities.Common.ContactDetails
{
    /// <summary>
    /// Contact details of the sub-entity.
    /// </summary>
    public class ContactDetails
    {
        /// <summary>
        /// The details of the user responsible for onboarding the sub-entity.
        /// [Optional] (not part of the US ISV Seller variants)
        /// </summary>
        public Invitee Invitee { get; set; }

        /// <summary>
        /// The phone number of the sub-entity.
        /// [Required] for every Accounts API v2.0 variant and the US ISV Seller variants; [Optional]
        /// for the other v3.0 variants.
        /// On v3.0, <see cref="Phone.CountryCode"/> is required and is the ISO 3166-1 alpha-2 country
        /// where the number is registered (for example "FR"), not the dialling code. v2.0 takes
        /// <see cref="Phone.Number"/> only.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Email addresses for this sub-entity.
        /// [Required] for every Accounts API v2.0 variant and the US ISV Seller variants; [Optional]
        /// for the other v3.0 variants.
        /// </summary>
        public EmailAddresses EmailAddresses { get; set; }
    }
}