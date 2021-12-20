using Checkout.Common;

namespace Checkout.Instruments
{
    public class InstrumentCustomerRequest : CustomerRequest
    {
        public bool Default { get; set; }

        public Phone Phone { get; set; }
    }
}