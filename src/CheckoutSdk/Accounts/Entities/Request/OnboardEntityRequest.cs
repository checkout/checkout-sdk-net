using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Accounts.Entities.Common.Documents;

namespace Checkout.Accounts.Entities.Request
{
    public class OnboardEntityRequest
    {
        /// <summary>
        /// A unique reference you can later use to identify this entity.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Information about the company represented by the sub-entity.
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Information about the profile of the sub-entity, primarily regarding the products/services
        /// offered.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Contact details of this sub-entity.
        /// </summary>
        public ContactDetails ContactDetails { get; set; }

        /// <summary>
        /// Verification documents for the sub-entity.
        /// </summary>
        public Documents Documents { get; set; }

        /// <summary>
        /// Information about the sub-entity's expected processing.
        /// </summary>
        public ProcessingDetails ProcessingDetails { get; set; }

        /// <summary>
        /// Whether to create the entity as a draft.
        /// Note: this property serializes to <c>draft</c>. Prefer <see cref="IsDraft"/> to control
        /// draft creation.
        /// </summary>
        public bool? Draft { get; set; }

        /// <summary>
        /// Whether to create the entity as a draft.
        /// </summary>
        public bool? IsDraft { get; set; }

        /// <summary>
        /// Information about the individual represented by the sub-entity.
        /// </summary>
        public Individual Individual { get; set; }

        // Unknown

        /// <summary>
        /// Additional information about the sub-entity.
        /// </summary>
        public AdditionalInfo AdditionalInfo { get; set; }

        /// <summary>
        /// Identifier of a seller category configured on your platform during onboarding.
        /// Categories define the pricing, capabilities, and risk profile applied to sub-entities;
        /// ask your Checkout.com contact for the list available to your platform.
        /// Used for US ISV onboarding variants.
        /// [Optional]
        /// </summary>
        public string SellerCategory { get; set; }

        /// <summary>
        /// Details of the person who agreed to the terms and conditions.
        /// Used for the SaaS onboarding variants (Accounts API v3.0).
        /// [Optional]
        /// </summary>
        public AgreedTerms AgreedTerms { get; set; }

        /// <summary>
        /// Captures evidence of the end-user's consent to onboarding.
        /// Used for US ISV onboarding variants.
        /// [Optional]
        /// </summary>
        public Submitter Submitter { get; set; }
    }
}