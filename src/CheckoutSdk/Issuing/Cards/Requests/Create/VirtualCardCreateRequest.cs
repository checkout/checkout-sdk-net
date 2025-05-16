using Checkout.Issuing.Cards.Controls;
using Checkout.Issuing.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public class VirtualCardCreateRequest : AbstractCardCreateRequest
    {
        public VirtualCardCreateRequest() : base(IssuingCardType.Virtual)
        {
        }
        
        public bool? IsSingleUse { get; set; }
        
        public IList<ReturnCredentialsType> ReturnCredentials { get; set; }
        
        public IList<string> ControlProfiles { get; set; }
        
        public IList<AbstractCardControls> Controls { get; set; }
        
    }
}