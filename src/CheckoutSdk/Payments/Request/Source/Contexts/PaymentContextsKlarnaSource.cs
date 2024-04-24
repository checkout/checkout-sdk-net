using Checkout.Common;

namespace Checkout.Payments.Request.Source.Contexts
{
    public class PaymentContextsKlarnaSource : AbstractRequestSource
    {
        public PaymentContextsKlarnaSource() : base(PaymentSourceType.Klarna)
        {
        }

        public AccountHolder AccountHolder { get; set; }
    }
}