using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Accounts.Entities.Common.Documents;

namespace Checkout.Accounts.Entities.Request
{
    public class OnboardEntityRequest
    {
        public string Reference { get; set; }

        public Company Company { get; set; }
        
        public Profile Profile { get; set; }

        public ContactDetails ContactDetails { get; set; }
        
        public Documents Documents { get; set; }

        public ProcessingDetails ProcessingDetails { get; set; }

        public bool? Draft { get; set; }
        
        public bool? IsDraft { get; set; }
        
        public Individual Individual { get; set; }

        // Unknown

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
        /// Captures evidence of the end-user's consent to onboarding.
        /// Used for US ISV onboarding variants.
        /// [Optional]
        /// </summary>
        public Submitter Submitter { get; set; }
    }
}