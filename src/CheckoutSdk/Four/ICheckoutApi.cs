using Checkout.Customers.Four;
using Checkout.Disputes.Four;
using Checkout.Forex;
using Checkout.Instruments.Four;
using Checkout.Payments.Four;
using Checkout.Risk;
using Checkout.Sessions;
using Checkout.Tokens;
using Checkout.Workflows.Four;

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

        IForexClient ForexClient();

        IWorkflowsClient WorkflowsClient();

        ISessionsClient SessionsClient();
    }
}