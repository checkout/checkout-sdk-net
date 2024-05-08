using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestIdealSource : AbstractRequestSource
    {
        public RequestIdealSource() : base(PaymentSourceType.Ideal)
        {
        }

        public string Description { get; set; }

        public string Language { get; set; }
    }
}