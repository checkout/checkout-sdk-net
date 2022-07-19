using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class OnboardEntityResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }

        public Capabilities Capabilities { get; set; }

        public OnboardingStatus? Status { get; set; }

        public IList<RequirementsDue> RequirementsDue { get; set; }
    }
}