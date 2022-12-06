using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public class RequestTokenSource : AbstractRequestSource
    {
        public RequestTokenSource() : base(PaymentSourceType.Token)
        {
        }

        public string Token { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
        
        public bool? Stored { get; set; }

        public bool? StoreForFutureUse { get; set; }
    }
}