using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestBalotoSource : AbstractRequestSource
    {
        public RequestBalotoSource() : base(PaymentSourceType.Baloto)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public BalotoPayer Payer { get; set; }

        public string Description { get; set; }
      
    }
}