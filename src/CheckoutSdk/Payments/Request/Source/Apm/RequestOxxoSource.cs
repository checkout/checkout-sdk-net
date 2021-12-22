using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestOxxoSource : AbstractRequestSource
    {
        public RequestOxxoSource() : base(PaymentSourceType.Oxxo)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }
       
    }
}