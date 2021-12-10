using Checkout.Customers.Four;
using Checkout.Disputes.Four;
using Checkout.Instruments.Four;
using Checkout.Payments.Four;
using Checkout.Risk;
using Checkout.Tokens;

namespace Checkout.Four
{
    public interface ICheckoutApi : ICheckoutApiClient
    {
        ITokensClient TokensClient();

        ICustomersClient CustomersClient();

        IPaymentsClient PaymentsClient();

        IInstrumentsClient InstrumentsClient();

        IDisputesClient DisputesClient();

        IRiskClient RiskClient();
    }
}