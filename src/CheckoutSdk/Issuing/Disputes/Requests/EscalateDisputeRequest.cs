using System.Collections.Generic;

namespace Checkout.Issuing.Disputes.Requests
{
    public class EscalateDisputeRequest
    {
        public string Justification { get; set; }

        public IList<DisputeEvidence> AdditionalEvidence { get; set; }

        public long? Amount { get; set; }

        public DisputeReasonChange ReasonChange { get; set; }
    }
}