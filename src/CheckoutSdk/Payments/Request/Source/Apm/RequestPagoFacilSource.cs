using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPagoFacilSource : AbstractRequestSource
    {
        public RequestPagoFacilSource() : base(PaymentSourceType.PagoFacil)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }
    }
}