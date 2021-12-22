using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestRapiPagoSource : AbstractRequestSource
    {
        public RequestRapiPagoSource() : base(PaymentSourceType.RapiPago)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }
              
    }
}