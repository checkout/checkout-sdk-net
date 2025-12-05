using Checkout.Accounts;
using Checkout.Authentication;
using Checkout.Balances;
using Checkout.Issuing;
using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Financial;
using Checkout.HandlePaymentsAndPayouts.Flow;
using Checkout.Forex;
using Checkout.Forward;
using Checkout.Instruments;
using Checkout.Metadata;
using Checkout.NetworkTokens;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Setups;
using Checkout.Reports;
using Checkout.Risk;
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

        IAuthenticationClient AuthenticationClient();

        IAccountsClient AccountsClient();

        IPaymentLinksClient PaymentLinksClient();

        IHostedPaymentsClient HostedPaymentsClient();

        IBalancesClient BalancesClient();

        ITransfersClient TransfersClient();

        IReportsClient ReportsClient();

        IMetadataClient MetadataClient();

        IFinancialClient FinancialClient();

        IIssuingClient IssuingClient();

        IPaymentContextsClient PaymentContextsClient();
        
        IForwardClient ForwardClient();
        
        IFlowClient FlowClient();
        
        INetworkTokensClient NetworkTokensClient();

        IPaymentSetupsClient PaymentSetupsClient();
    }
}
