using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestEpsSource : AbstractRequestSource
    {
        public string Purpose { get; set; }

        public string Bic { get; set; }

        public RequestEpsSource() : base(PaymentSourceType.EPS)
        {
        }
    }
}