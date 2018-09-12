using Checkout.Sdk.Payments;
using Checkout.Sdk.Tokens;

namespace Checkout.Sdk
{
    public interface ICheckoutApi
    {
        IPaymentsClient Payments { get; }
        ITokensClient Tokens { get; }
    }
}