using Checkout.Common;

namespace Checkout.Payments.Previous.Response.Source
{
    public interface IResponseSource
    {
        PaymentSourceType? Type();
    }
}