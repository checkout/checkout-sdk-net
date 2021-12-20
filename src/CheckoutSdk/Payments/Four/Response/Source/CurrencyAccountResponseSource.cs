using Checkout.Common;

namespace Checkout.Payments.Four.Response.Source
{
    public class CurrencyAccountResponseSource : AbstractResponseSource, IResponseSource
    {
        public long? Amount { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}