using Checkout.Accounts;
using Checkout.ApplePay;
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
using Checkout.PaymentMethods;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Setups;
using Checkout.Reports;
using Checkout.Risk;
using Checkout.StandaloneAccountUpdater;
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
        private readonly IAuthenticationClient _authenticationClient;
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
        private readonly IForwardClient _forwardClient;
        private readonly IFlowClient _flowClient;
        private readonly INetworkTokensClient _networkTokensClient;
        private readonly IPaymentSetupsClient _paymentSetupsClient;
        private readonly IApplePayClient _applePayClient;
        private readonly IPaymentMethodsClient _paymentMethodsClient;
        private readonly IStandaloneAccountUpdaterClient _standaloneAccountUpdaterClient;

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
            _authenticationClient = new AuthenticationClient(baseApiClient, configuration);
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
            _forwardClient = new ForwardClient(baseApiClient, configuration);
            _flowClient = new FlowClient(baseApiClient, configuration);
            _networkTokensClient = new NetworkTokensClient(baseApiClient, configuration);
            _paymentSetupsClient = new PaymentSetupsClient(baseApiClient, configuration);
            _applePayClient = new ApplePayClient(baseApiClient, configuration);
            _paymentMethodsClient = new PaymentMethodsClient(baseApiClient, configuration);
            _standaloneAccountUpdaterClient = new StandaloneAccountUpdaterClient(baseApiClient, configuration);
        }

        private static ApiClient BaseApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.EnvironmentSubdomain != null
                    ? configuration.EnvironmentSubdomain.ApiUri
                    : configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri,
                    configuration.RecordTelemetry);
        }

        private static ApiClient FilesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().FilesApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient TransfersApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().TransfersApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient BalancesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().BalancesApiUri,
                configuration.RecordTelemetry);
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

        public IAuthenticationClient AuthenticationClient()
        {
            return _authenticationClient;
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

        public IForwardClient ForwardClient()
        {
            return _forwardClient;
        }

        public IFlowClient FlowClient()
        {
            return _flowClient;
        }

        public INetworkTokensClient NetworkTokensClient()
        {
            return _networkTokensClient;
        }

        public IPaymentSetupsClient PaymentSetupsClient()
        {
            return _paymentSetupsClient;
        }
        
        public IApplePayClient ApplePayClient()
        {
            return _applePayClient;
        }
        
        public IPaymentMethodsClient PaymentMethodsClient()
        {
            return _paymentMethodsClient;
        }
        
        public IStandaloneAccountUpdaterClient StandaloneAccountUpdaterClient()
        {
            return _standaloneAccountUpdaterClient;
        }
        
    }
}
