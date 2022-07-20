using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestRapiPagoSource : AbstractRequestSource
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