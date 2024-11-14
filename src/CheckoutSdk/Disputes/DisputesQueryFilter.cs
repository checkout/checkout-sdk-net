using System;

namespace Checkout.Disputes
{
    public class DisputesQueryFilter
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Id { get; set; }

        public string Statuses { get; set; }

        public string PaymentId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string ThisChannelOnly { get; set; }
        
        //Not available on Previous
        
        public string EntityIds { get; set; }

        public string SubEntityIds { get; set; }

        public string PaymentMcc { get; set; }
        
        public string ProcessingChannelIds { get; set; }
        
        public string SegmentIds { get; set; }
    }
}