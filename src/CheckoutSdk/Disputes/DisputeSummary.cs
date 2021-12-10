using Checkout.Common;
using System;

namespace Checkout.Disputes
{
    public sealed class DisputeSummary : Resource
    {
        public string Id { get; set; }

        public DisputeCategory? Category { get; set; }

        public DisputeStatus? Status { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public string PaymentId { get; set; }

        public string PaymentActionId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime? EvidenceRequiredBy { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}