using Checkout.Customers.Four;
using Checkout.Disputes.Four;
using Checkout.Files;
using Checkout.Forex;
using Checkout.Instruments.Four;
using Checkout.Marketplace;
using Checkout.Payments.Four;
using Checkout.Risk;
using Checkout.Tokens;
using Checkout.Workflows.Four;

namespace Checkout.Four
{
    public class CheckoutApi : ICheckoutApi
    {
        private readonly IFilesClient _fileClient;
        private readonly ITokensClient _tokensClient;
        private readonly ICustomersClient _customersClient;
        private readonly IPaymentsClient _paymentsClient;
        private readonly IInstrumentsClient _instrumentsClient;
        private readonly IDisputesClient _disputesClient;
        private readonly IRiskClient _riskClient;
        private readonly IForexClient _forexClient;
        private readonly IWorkflowsClient _workflowsClient;
        private readonly IMarketplaceClient _marketplaceClient;

        public CheckoutApi(CheckoutConfiguration configuration)
        {
            var apiClient = new ApiClient(configuration);

            _fileClient = new FilesClient(apiClient, configuration);
            _tokensClient = new TokensClient(apiClient, configuration);
            _customersClient = new CustomersClient(apiClient, configuration);
            _paymentsClient = new PaymentsClient(apiClient, configuration);
            _instrumentsClient = new InstrumentsClient(apiClient, configuration);
            _disputesClient = new DisputesClient(apiClient, configuration);
            _riskClient = new RiskClient(apiClient, configuration);
            _forexClient = new ForexClient(apiClient, configuration);
            _workflowsClient = new WorkflowsClient(apiClient, configuration);
            _marketplaceClient = new MarketplaceClient(apiClient, configuration, _fileClient);
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

        public IMarketplaceClient MarketplaceClient()
        {
            return _marketplaceClient;
        }
    }
}