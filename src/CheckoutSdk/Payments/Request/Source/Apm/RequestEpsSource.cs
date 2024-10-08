using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestEpsSource : AbstractRequestSource
    {
        public string Purpose { get; set; }
        
        public AccountHolder AccountHolder { get; set; }

        public RequestEpsSource() : base(PaymentSourceType.EPS)
        {
        }
    }
}