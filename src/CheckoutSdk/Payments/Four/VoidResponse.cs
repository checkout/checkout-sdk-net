using Checkout.Common;

namespace Checkout.Payments.Four
{
    public sealed class VoidResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

    }
}