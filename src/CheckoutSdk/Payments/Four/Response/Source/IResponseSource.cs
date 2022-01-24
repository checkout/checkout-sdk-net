using Checkout.Common;

namespace Checkout.Payments.Four.Response.Source
{
    public interface IResponseSource
    {
        PaymentSourceType? Type();
    }
}