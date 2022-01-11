using Checkout.Common;

namespace Checkout.Payments.Four.Sender
{
    public class Identification
    {
        public IdentificationType? Type { get; set; }

        public string Number { get; set; }

        public CountryCode? IssuingCountry { get; set; }
    }
}