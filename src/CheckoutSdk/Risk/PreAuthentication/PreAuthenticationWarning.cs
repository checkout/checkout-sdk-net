using System.Collections.Generic;

namespace Checkout.Risk.PreAuthentication
{
    public class PreAuthenticationWarning
    {
        public PreAuthenticationDecision? Decision { get; set; }

        public IList<string> Reasons { get; set; }
    }
}