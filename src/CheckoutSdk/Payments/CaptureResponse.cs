using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class CaptureResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

    }
}