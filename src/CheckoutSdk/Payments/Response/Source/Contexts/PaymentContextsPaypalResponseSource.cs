using Checkout.Common;

namespace Checkout.Payments.Response.Source.Contexts
{
    public class PaymentContextsPaypalResponseSource : AbstractPaymentContextsResponseSource, IResponseSource
    {
        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}