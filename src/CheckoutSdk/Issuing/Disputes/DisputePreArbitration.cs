using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    public class DisputePreArbitration
    {
        public DateTime? SubmittedOn { get; set; }

        public IList<DisputeFileEvidence> Evidence { get; set; }

        public DisputeAmount Amount { get; set; }

        public DisputeReasonChange ReasonChange { get; set; }

        public string Justification { get; set; }

        public DateTime? MerchantRespondedOn { get; set; }

        public IList<DisputeFileEvidence> MerchantEvidence { get; set; }
    }
}