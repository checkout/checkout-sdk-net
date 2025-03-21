using Checkout.Customers.Previous;
using Checkout.Disputes;
using Checkout.Events.Previous;
using Checkout.Instruments.Previous;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Previous;
using Checkout.Reconciliation.Previous;
using Checkout.Risk;
using Checkout.Sources.Previous;
using Checkout.Tokens;
using Checkout.Webhooks.Previous;

namespace Checkout.Previous
{
    public class CheckoutApi : AbstractCheckoutApmApi, ICheckoutApi
    {
        private readonly ITokensClient _tokensClient;
        private readonly ICustomersClient _customersClient;
        private readonly ISourcesClient _sourcesClient;
        private readonly IPaymentsClient _paymentsClient;
        private readonly IInstrumentsClient _instrumentsClient;
        private readonly IDisputesClient _disputesClient;
        private readonly IWebhooksClient _webhooksClient;
        private readonly IEventsClient _eventsClient;
        private readonly IRiskClient _riskClient;
        private readonly IPaymentLinksClient _paymentLinksClient;
        private readonly IReconciliationClient _reconciliationClient;
        private readonly IHostedPaymentsClient _hostedPaymentsClient;

        public CheckoutApi(CheckoutConfiguration configuration) : base(configuration)
        {
            var apiClient = new ApiClient(configuration.HttpClientFactory,
                configuration.EnvironmentSubdomain != null
                    ? configuration.EnvironmentSubdomain.ApiUri
                    : configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri,
                    configuration.RecordTelemetry);
            _tokensClient = new TokensClient(apiClient, configuration);
            _customersClient = new CustomersClient(apiClient, configuration);
            _sourcesClient = new SourcesClient(apiClient, configuration);
            _paymentsClient = new PaymentsClient(apiClient, configuration);
            _instrumentsClient = new InstrumentsClient(apiClient, configuration);
            _disputesClient = new DisputesClient(apiClient, configuration);
            _webhooksClient = new WebhooksClient(apiClient, configuration);
            _eventsClient = new EventsClient(apiClient, configuration);
            _riskClient = new RiskClient(apiClient, configuration);
            _paymentLinksClient = new PaymentLinksClient(apiClient, configuration);
            _reconciliationClient = new ReconciliationClient(apiClient, configuration);
            _hostedPaymentsClient = new HostedPaymentsClient(apiClient, configuration);
        }

        public ITokensClient TokensClient()
        {
            return _tokensClient;
        }

        public ICustomersClient CustomersClient()
        {
            return _customersClient;
        }

        public ISourcesClient SourcesClient()
        {
            return _sourcesClient;
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

        public IWebhooksClient WebhooksClient()
        {
            return _webhooksClient;
        }

        public IEventsClient EventsClient()
        {
            return _eventsClient;
        }

        public IRiskClient RiskClient()
        {
            return _riskClient;
        }

        public IPaymentLinksClient PaymentLinksClient()
        {
            return _paymentLinksClient;
        }

        public IReconciliationClient ReconciliationClient()
        {
            return _reconciliationClient;
        }

        public IHostedPaymentsClient HostedPaymentsClient()
        {
            return _hostedPaymentsClient;
        }
    }
}