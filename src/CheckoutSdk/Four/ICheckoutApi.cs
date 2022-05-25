using Checkout.Customers.Four;
using Checkout.Disputes;
using Checkout.Forex;
using Checkout.Instruments.Four;
using Checkout.Marketplace;
using Checkout.Payments.Four;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
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

        IMarketplaceClient MarketplaceClient();

        IPaymentLinksClient PaymentLinksClient();

        IHostedPaymentsClient HostedPaymentsClient();
    }
}