using Checkout.Common;

namespace Checkout.Payments.Hosted
{
    public sealed class HostedPaymentResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }
    }
}