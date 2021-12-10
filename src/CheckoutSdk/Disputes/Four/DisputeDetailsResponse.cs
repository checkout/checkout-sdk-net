using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Disputes.Four
{
    public sealed class DisputeDetailsResponse
    {
        public string Id { get; set; }

        public string EntityId { get; set; }

        public string SubEntityId { get; set; }

        public DisputeCategory? Category { get; set; }

        public int? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public DisputeStatus? Status { get; set; }

        public DisputeResolvedReason? ResolvedReason { get; set; }

        public IList<DisputeRelevantEvidence> RelevantEvidence { get; set; }

        public DateTime? EvidenceRequiredBy { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public DateTime? LastUpdate { get; set; }

        public PaymentDispute Payment { get; set; }
    }
}