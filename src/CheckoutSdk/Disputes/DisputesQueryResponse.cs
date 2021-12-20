using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class DisputesQueryResponse
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Id { get; set; }

        public string Statuses { get; set; }

        public string PaymentId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string ThisChannelOnly { get; set; }

        public int? TotalCount { get; set; }

        public IList<DisputeSummary> Data { get; set; }
    }
}