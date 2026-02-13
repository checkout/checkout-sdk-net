using System.Collections.Generic;

namespace Checkout.Issuing.Disputes.Requests
{
    public class CreateDisputeRequest
    {
        public string TransactionId { get; set; }

        public string Reason { get; set; }

        public IList<DisputeEvidence> Evidence { get; set; }

        public long? Amount { get; set; }

        public string PresentmentMessageId { get; set; }

        public bool? IsReadyForSubmission { get; set; }

        public string Justification { get; set; }
    }
}