using System.Collections.Generic;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationWarning
    {
        public PreAuthenticationDecision? Decision { get; set; }

        public IList<string> Reasons { get; set; }
    }
}