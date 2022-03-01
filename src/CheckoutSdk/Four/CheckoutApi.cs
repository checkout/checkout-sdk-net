using Checkout.Customers.Four;
using Checkout.Disputes.Four;
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
        private readonly IMarketplaceClient _marketplaceClient;
        private readonly IPaymentLinksClient _paymentLinksClient;
        private readonly IHostedPaymentsClient _hostedPaymentsClient;

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
            IApiClient apiFilesClient = null;
            if (configuration.FilesApiConfiguration != null)
            {
                apiFilesClient = ApiFilesClient(configuration);
            }

            _marketplaceClient = new MarketplaceClient(
                baseApiClient,
                apiFilesClient,
                TransfersApiClient(configuration),
                configuration);
            _paymentLinksClient = new PaymentLinksClient(baseApiClient, configuration);
            _hostedPaymentsClient = new HostedPaymentsClient(baseApiClient, configuration);
        }

        private static ApiClient BaseApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri);
        }

        private static ApiClient ApiFilesClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.FilesApiConfiguration.Environment.GetAttribute<EnvironmentAttribute>().FilesApiUri);
        }

        private static ApiClient TransfersApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().TransfersApiUri);
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

        public IMarketplaceClient MarketplaceClient()
        {
            return _marketplaceClient;
        }

        public IPaymentLinksClient PaymentLinksClient()
        {
            return _paymentLinksClient;
        }

        public IHostedPaymentsClient HostedPaymentsClient()
        {
            return _hostedPaymentsClient;
        }
    }
}