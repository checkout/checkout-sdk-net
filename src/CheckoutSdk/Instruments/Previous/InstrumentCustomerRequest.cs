using Checkout.Common;

namespace Checkout.Instruments.Previous
{
    public class InstrumentCustomerRequest : CustomerRequest
    {
        public string Id { get; set; }
        public bool Default { get; set; }
    }
}