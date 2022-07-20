using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestSepaSource : AbstractRequestSource
    {
        public RequestSepaSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }
    }
}