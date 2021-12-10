using Checkout.Common;

namespace Checkout.Payments.Four
{
    public sealed class CaptureResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }
      
    }
}