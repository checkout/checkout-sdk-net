using Checkout.Common;

namespace Checkout.Payments.Response.Source
{
    public class CurrencyAccountResponseSource : AbstractResponseSource, IResponseSource
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}