using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Links
{
    public class PaymentLinkResponse : Resource
    {
        public string Id { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public string Reference { get; set; }

        public IList<object> Warnings { get; set; }
    }
}