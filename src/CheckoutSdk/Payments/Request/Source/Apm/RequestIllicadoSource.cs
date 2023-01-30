using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestIllicadoSource : AbstractRequestSource
    {
        public Address BillingAddress { get; set; }

        public RequestIllicadoSource() : base(PaymentSourceType.Illicado)
        {
        }
    }
}