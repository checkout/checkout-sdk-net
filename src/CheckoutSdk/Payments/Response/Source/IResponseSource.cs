using Checkout.Common;

namespace Checkout.Payments.Response.Source
{
    public interface IResponseSource
    {
        PaymentSourceType? Type();
    }
}