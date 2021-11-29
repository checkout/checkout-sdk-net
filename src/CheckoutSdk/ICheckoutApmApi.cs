using Checkout.Apm.Ideal;
using Checkout.Apm.Klarna;
using Checkout.Apm.Sepa;

namespace Checkout
{
    public interface ICheckoutApmApi
    {
        IIdealClient IdealClient();

        IKlarnaClient KlarnaClient();

        ISepaClient SepaClient();
    }
}