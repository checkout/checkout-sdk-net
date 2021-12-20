using Checkout.Common;

namespace Checkout.Payments.Four
{
    public class RefundResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }
    }
}