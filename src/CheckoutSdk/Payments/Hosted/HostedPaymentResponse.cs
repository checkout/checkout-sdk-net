using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }

        public IList<object> Warnings { get; set; }
    }
}