using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class DisputeDetailsResponse
    {
        public string Id { get; set; }

        public DisputeCategory? Category { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public DisputeStatus? Status { get; set; }

        public DisputeResolvedReason? ResolvedReason { get; set; }

        public IList<DisputeRelevantEvidence> RelevantEvidence { get; set; }

        public string EvidenceRequiredBy { get; set; }

        public string ReceivedOn { get; set; }

        public string LastUpdate { get; set; }

        public PaymentDispute Payment { get; set; }
    }
}