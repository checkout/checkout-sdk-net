using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestGiropaySource : AbstractRequestSource
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }

        public string Purpose { get; set; }
     
    }
}