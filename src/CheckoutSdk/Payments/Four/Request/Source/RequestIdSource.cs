using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source
{
    public class RequestIdSource : AbstractRequestSource
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }

        public string PaymentMethod { get; set; }
    }
}