using Checkout.Common;

namespace Checkout.Payments.Four
{
    public class VoidResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

    }
}