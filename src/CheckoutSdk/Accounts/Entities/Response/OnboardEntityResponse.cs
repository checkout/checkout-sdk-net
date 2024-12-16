using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    public class OnboardEntityResponse : Resource
    {
        // Common
        public string Id { get; set; }
        public string Reference { get; set; }
        
        public IList<RequirementsDue> RequirementsDue { get; set; }
        
        // Company Platform (2.0)
        
        public Capabilities Capabilities { get; set; }
        
        // Unknown
        
        public OnboardingStatus? Status { get; set; }
        
    }
}