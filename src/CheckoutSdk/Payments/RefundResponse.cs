using Checkout.Sdk.Common;

namespace Checkout.Sdk.Payments
{
    public class RefundResponse : Resource
    {
        public string ActionId { get; set; }
        public string Reference { get; set; }
    }
}