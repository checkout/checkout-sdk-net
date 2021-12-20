using Checkout.Common;
using System;

namespace Checkout.Payments.Links
{
    public class PaymentLinkResponse : Resource
    {
        public string Id { get; set; }

        public string PaymentId { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public string Reference { get; set; }
            
    }
}