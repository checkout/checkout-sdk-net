using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBalotoSource : AbstractRequestSource
    {
        public RequestBalotoSource() : base(PaymentSourceType.Baloto)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }
    }
}