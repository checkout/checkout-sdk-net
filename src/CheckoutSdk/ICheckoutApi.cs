using Checkout.Accounts;
using Checkout.Balances;
using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Forex;
using Checkout.Instruments;
using Checkout.Metadata;
using Checkout.Payments;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Reports;
using Checkout.Risk;
using Checkout.Sessions;
using Checkout.Tokens;
using Checkout.Transfers;
using Checkout.Workflows;

namespace Checkout
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

        IAccountsClient AccountsClient();

        IPaymentLinksClient PaymentLinksClient();

        IHostedPaymentsClient HostedPaymentsClient();
        
        IBalancesClient BalancesClient();

        ITransfersClient TransfersClient();

        IReportsClient ReportsClient();

        IMetadataClient MetadataClient();
    }
}
