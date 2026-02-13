using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    public class DisputeChargeback
    {
        public DateTime? SubmittedOn { get; set; }

        public string Reason { get; set; }

        public DisputeAmount Amount { get; set; }

        public IList<DisputeFileEvidence> Evidence { get; set; }

        public string Justification { get; set; }
    }
}