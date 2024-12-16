using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    public class OnboardEntityDetailsResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }

        public Capabilities Capabilities { get; set; }

        public string Status { get; set; }

        public IList<RequirementsDue> RequirementsDue { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public Profile Profile { get; set; }
        
        public Company Company { get; set; }

        public Individual Individual { get; set; }

        public IList<Instrument> Instruments { get; set; }
    }
}