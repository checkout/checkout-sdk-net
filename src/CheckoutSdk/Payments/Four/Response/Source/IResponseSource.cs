using Checkout.Common;

namespace Checkout.Payments.Four.Response.Source
{
    public interface IResponseSource
    {
        public PaymentSourceType? Type();
    }
}