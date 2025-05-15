using Checkout.Issuing.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class VirtualCardCreateResponse : AbstractCardCreateResponse
    {
        public VirtualCardCreateResponse() : base(IssuingCardType.Virtual)
        {
        }
        
        public Credentials Credentials { get; set; }

        public IList<AbstractCardControlsResponse> Controls { get; set; }
    }
}