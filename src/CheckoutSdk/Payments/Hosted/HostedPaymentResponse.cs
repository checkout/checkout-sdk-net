using Checkout.Common;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }
    }
}