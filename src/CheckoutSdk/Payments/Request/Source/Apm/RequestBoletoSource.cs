using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBoletoSource : AbstractRequestSource
    {
        public RequestBoletoSource() : base(PaymentSourceType.Boleto)
        {
        }

        public IntegrationType? IntegrationType { get; set; }

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }
    }
}