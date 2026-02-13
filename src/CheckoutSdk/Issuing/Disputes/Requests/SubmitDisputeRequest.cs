using System.Collections.Generic;

namespace Checkout.Issuing.Disputes.Requests
{
    public class SubmitDisputeRequest
    {
        public string Reason { get; set; }

        public IList<DisputeEvidence> Evidence { get; set; }

        public long? Amount { get; set; }
    }
}