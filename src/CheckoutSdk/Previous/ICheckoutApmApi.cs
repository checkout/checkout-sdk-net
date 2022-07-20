using Checkout.Apm.Ideal;
using Checkout.Apm.Previous.Klarna;
using Checkout.Apm.Previous.Sepa;

namespace Checkout.Previous
{
    public interface ICheckoutApmApi
    {
        IIdealClient IdealClient();

        IKlarnaClient KlarnaClient();

        ISepaClient SepaClient();
    }
}