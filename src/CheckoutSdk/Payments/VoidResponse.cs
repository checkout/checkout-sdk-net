using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class VoidResponse : Resource
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

    }
}