using Checkout.Common;
using System;

namespace Checkout.Issuing.Disputes.Responses
{
    public class IssuingDisputeResponse : Resource
    {
        public string Id { get; set; }

        public string Reason { get; set; }

        public DisputeAmount DisputedAmount { get; set; }

        public IssuingDisputeStatus? Status { get; set; }

        public IssuingDisputeStatusReason? StatusReason { get; set; }

        public string TransactionId { get; set; }

        public string PresentmentMessageId { get; set; }

        public DisputeMerchant Merchant { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DisputeChargeback Chargeback { get; set; }

        public DisputeRepresentment Representment { get; set; }

        public DisputePreArbitration PreArbitration { get; set; }

        public DisputeArbitration Arbitration { get; set; }
    }
}