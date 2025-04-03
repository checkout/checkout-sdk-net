using Checkout.Common;
using System;

namespace Checkout.Issuing.ControlProfiles.Responses
{
    public class ControlProfileResponse : Resource
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? LastModifiedDate { get; set; }
    }
}