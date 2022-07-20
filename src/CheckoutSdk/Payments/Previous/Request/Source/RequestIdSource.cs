using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source
{
    public class RequestIdSource : AbstractRequestSource
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }
               
    }
}