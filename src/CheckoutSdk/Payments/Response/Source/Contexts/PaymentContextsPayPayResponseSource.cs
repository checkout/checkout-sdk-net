using Checkout.Common;

namespace Checkout.Payments.Response.Source.Contexts
{
    public class PaymentContextsPayPayResponseSource : AbstractPaymentContextsResponseSource, IResponseSource
    {
        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}