using Checkout.Common;

namespace Checkout.Payments
{
    public class CaptureResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }
    }
}