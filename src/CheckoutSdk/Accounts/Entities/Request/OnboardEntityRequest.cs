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

        public bool Draft { get; set; }
        public bool IsDraft { get; set; }
        
        public Individual Individual { get; set; }
        
        // Unknown
        
        public AdditionalInfo AdditionalInfo { get; set; }
    }
}