using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    public class DisputeRepresentment
    {
        public DateTime? ReceivedOn { get; set; }

        public DisputeAmount Amount { get; set; }

        public IList<DisputeFileEvidence> Evidence { get; set; }
    }
}