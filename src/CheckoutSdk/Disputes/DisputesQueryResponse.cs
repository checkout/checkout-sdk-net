using System;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class DisputesQueryResponse : HttpMetadata
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Id { get; set; }

        public string Statuses { get; set; }

        public string PaymentId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string ThisChannelOnly { get; set; }

        public int? TotalCount { get; set; }

        public IList<DisputeSummary> Data { get; set; }
        
        //Not available on Previous
        
        public string EntityIds { get; set; }

        public string SubEntityIds { get; set; }

        public string PaymentMcc { get; set; }
        
        public string ProcessingChannelIds { get; set; }
        
        public string SegmentIds { get; set; }
    }
}