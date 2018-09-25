using Checkout.Payments;
using Checkout.Tokens;

namespace Checkout
{
    public interface ICheckoutApi
    {
        IPaymentsClient Payments { get; }
        ITokensClient Tokens { get; }
        string PublicKey { get; }
    }
}