using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPlaidSource : AbstractRequestSource
    {
        public string Token { get; set; }
        
        public AccountHolder AccountHolder { get; set; }

        public RequestPlaidSource() : base(PaymentSourceType.Plaid)
        {
        }
    }
}