using Checkout.Common;

namespace Checkout.Instruments
{
    public sealed class InstrumentAccountHolder
    {
        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
    }
}