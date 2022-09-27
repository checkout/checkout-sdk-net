using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestKlarnaSource : AbstractRequestSource
    {
        public RequestKlarnaSource() : base(PaymentSourceType.Klarna)
        {
        }
        
        public AccountHolder AccountHolder { get; set; }
    }
}