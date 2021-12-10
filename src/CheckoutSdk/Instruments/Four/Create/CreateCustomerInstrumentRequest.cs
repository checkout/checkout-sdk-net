using Checkout.Common;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateCustomerInstrumentRequest
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public bool Default { get; set; }
    }
}