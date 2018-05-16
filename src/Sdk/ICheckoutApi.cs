using Checkout.Payments;

namespace Checkout
{
    public interface ICheckoutApi
    {
        IPaymentsClient Payments { get; }
    }
}