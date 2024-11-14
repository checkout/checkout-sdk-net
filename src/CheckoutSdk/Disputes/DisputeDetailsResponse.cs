using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class DisputeDetailsResponse : HttpMetadata
    {
        public string Id { get; set; }
        
        public string Reference { get; set; }

        public DisputeCategory? Category { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public DisputeStatus? Status { get; set; }

        public DisputeResolvedReason? ResolvedReason { get; set; }

        public IList<DisputeRelevantEvidence> RelevantEvidence { get; set; }

        public DateTime? EvidenceRequiredBy { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public DateTime? LastUpdate { get; set; }
        
        public bool? IsCeCandidate { get; set; }

        public PaymentDispute Payment { get; set; }
        
        public IList<EvidenceList> EvidenceList { get; set; }
        
        public IList<EvidenceBundle> EvidenceBundle { get; set; }
        
        public string SegmentId { get; set; }
        
        //Not available on Previous

        public string EntityId { get; set; }

        public string SubEntityId { get; set; }
    }
}