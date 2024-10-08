using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBizumSource : AbstractRequestSource
    {
        public string MobileNumber { get; set; }

        public RequestBizumSource() : base(PaymentSourceType.Bizum)
        {
        }
    }
}