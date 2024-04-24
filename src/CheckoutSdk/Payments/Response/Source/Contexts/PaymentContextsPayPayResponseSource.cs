using Checkout.Common;
using System;

namespace Checkout.Payments.Response.Source.Contexts
{
    [Obsolete("This property will be removed in the future, and should not be used. Use PaymentContextsPaypalResponseSource instead.", false)]

    public class PaymentContextsPayPayResponseSource : AbstractPaymentContextsResponseSource, IResponseSource
    {
        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}