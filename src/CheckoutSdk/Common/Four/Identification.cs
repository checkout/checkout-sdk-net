using Checkout.Payments.Four.Sender;

namespace Checkout.Common.Four
{
    public class Identification
    {
        public IdentificationType? Type { get; set; }

        public string Number { get; set; }

        public CountryCode? IssuingCountry { get; set; }

        public string DateOfExpiry { get; set; }
    }
}