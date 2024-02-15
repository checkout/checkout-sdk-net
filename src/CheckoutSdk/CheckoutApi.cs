using Checkout.Accounts;
using Checkout.Balances;
using Checkout.Issuing;
using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Financial;
using Checkout.Forex;
using Checkout.Instruments;
using Checkout.Metadata;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Sessions;
using Checkout.Reports;
using Checkout.Risk;
using Checkout.Sessions;
using Checkout.Tokens;
using Checkout.Transfers;
using Checkout.Workflows;

namespace Checkout
{
    public class CheckoutApi : ICheckoutApi
    {
        private readonly ITokensClient _tokensClient;
        private readonly ICustomersClient _customersClient;
        private readonly IPaymentsClient _paymentsClient;
        private readonly IInstrumentsClient _instrumentsClient;
        private readonly IDisputesClient _disputesClient;
        private readonly IRiskClient _riskClient;
        private readonly IForexClient _forexClient;
        private readonly IWorkflowsClient _workflowsClient;
        private readonly ISessionsClient _sessionsClient;
        private readonly IAccountsClient _accountsClient;
        private readonly IPaymentLinksClient _paymentLinksClient;
        private readonly IHostedPaymentsClient _hostedPaymentsClient;
        private readonly IBalancesClient _balancesClient;
        private readonly ITransfersClient _transfersClient;
        private readonly IReportsClient _reportsClient;
        private readonly IMetadataClient _metadataClient;
        private readonly IFinancialClient _financialClient;
        private readonly IIssuingClient _issuingClient;
        private readonly IPaymentContextsClient _paymentContextsClient;
        private readonly IPaymentSessionsClient _paymentSessionsClient;

        public CheckoutApi(CheckoutConfiguration configuration)
        {
            var baseApiClient = BaseApiClient(configuration);
            _tokensClient = new TokensClient(baseApiClient, configuration);
            _customersClient = new CustomersClient(baseApiClient, configuration);
            _paymentsClient = new PaymentsClient(baseApiClient, configuration);
            _instrumentsClient = new InstrumentsClient(baseApiClient, configuration);
            _disputesClient = new DisputesClient(baseApiClient, configuration);
            _riskClient = new RiskClient(baseApiClient, configuration);
            _forexClient = new ForexClient(baseApiClient, configuration);
            _workflowsClient = new WorkflowsClient(baseApiClient, configuration);
            _sessionsClient = new SessionsClient(baseApiClient, configuration);
            _accountsClient = new AccountsClient(
                baseApiClient,
                FilesApiClient(configuration),
                configuration);
            _paymentLinksClient = new PaymentLinksClient(baseApiClient, configuration);
            _hostedPaymentsClient = new HostedPaymentsClient(baseApiClient, configuration);
            _balancesClient = new BalancesClient(BalancesApiClient(configuration),
                configuration);
            _transfersClient = new TransfersClient(TransfersApiClient(configuration),
                configuration);
            _reportsClient = new ReportsClient(baseApiClient, configuration);
            _metadataClient = new MetadataClient(baseApiClient, configuration);
            _financialClient = new FinancialClient(baseApiClient, configuration);
            _issuingClient = new IssuingClient(baseApiClient, configuration);
            _paymentContextsClient = new PaymentContextsClient(baseApiClient, configuration);
            _paymentSessionsClient = new PaymentSessionsClient(baseApiClient, configuration);
        }

        private static ApiClient BaseApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.EnvironmentSubdomain != null
                    ? configuration.EnvironmentSubdomain.ApiUri
                    : configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri);
        }

        private static ApiClient FilesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().FilesApiUri);
        }

        private static ApiClient TransfersApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().TransfersApiUri);
        }

        private static ApiClient BalancesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().BalancesApiUri);
        }


        public ITokensClient TokensClient()
        {
            return _tokensClient;
        }

        public ICustomersClient CustomersClient()
        {
            return _customersClient;
        }

        public IPaymentsClient PaymentsClient()
        {
            return _paymentsClient;
        }

        public IInstrumentsClient InstrumentsClient()
        {
            return _instrumentsClient;
        }

        public IDisputesClient DisputesClient()
        {
            return _disputesClient;
        }

        public IRiskClient RiskClient()
        {
            return _riskClient;
        }

        public IForexClient ForexClient()
        {
            return _forexClient;
        }

        public IWorkflowsClient WorkflowsClient()
        {
            return _workflowsClient;
        }

        public ISessionsClient SessionsClient()
        {
            return _sessionsClient;
        }

        public IAccountsClient AccountsClient()
        {
            return _accountsClient;
        }

        public IPaymentLinksClient PaymentLinksClient()
        {
            return _paymentLinksClient;
        }

        public IHostedPaymentsClient HostedPaymentsClient()
        {
            return _hostedPaymentsClient;
        }

        public IBalancesClient BalancesClient()
        {
            return _balancesClient;
        }

        public ITransfersClient TransfersClient()
        {
            return _transfersClient;
        }

        public IReportsClient ReportsClient()
        {
            return _reportsClient;
        }

        public IMetadataClient MetadataClient()
        {
            return _metadataClient;
        }

        public IFinancialClient FinancialClient()
        {
            return _financialClient;
        }

        public IIssuingClient IssuingClient()
        {
            return _issuingClient;
        }

        public IPaymentContextsClient PaymentContextsClient()
        {
            return _paymentContextsClient;
        }

        public IPaymentSessionsClient PaymentSessionsClient()
        {
            return _paymentSessionsClient;
        }
    }
}