using Checkout.Common;

namespace Checkout.Payments
{
    public class DLocalProcessingSettings
    {
        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public Installments installments { get; set; }
    }

    public class Installments
    {
        public string Count { get; set; }
    }
}