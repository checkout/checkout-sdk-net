using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestIdealSource : AbstractRequestSource
    {
        public RequestIdealSource() : base(PaymentSourceType.Ideal)
        {
        }

        public string Bic { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }
    }
}