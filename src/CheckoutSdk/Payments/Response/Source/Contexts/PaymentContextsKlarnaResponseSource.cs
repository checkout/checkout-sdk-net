using Checkout.Common;

namespace Checkout.Payments.Response.Source.Contexts
{
    public class PaymentContextsKlarnaResponseSource : AbstractPaymentContextsResponseSource, IResponseSource
    {
        public new PaymentSourceType? Type()
        {
            return base.Type;
        }

        public AccountHolder AccountHolder { get; set; }
    }
}