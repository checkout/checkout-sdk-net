using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public class RequestProviderTokenSource : AbstractRequestSource
    {
        public RequestProviderTokenSource() : base(PaymentSourceType.ProviderToken)
        {
        }

        public string PaymentMethod { get; set; }

        public string Token { get; set; }

        public AccountHolder AccountHolder { get; set; }
    }
}