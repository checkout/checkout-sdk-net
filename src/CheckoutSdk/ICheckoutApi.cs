using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Events;
using Checkout.Instruments;
using Checkout.Payments;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Reconciliation;
using Checkout.Risk;
using Checkout.Sources;
using Checkout.Tokens;
using Checkout.Webhooks;

namespace Checkout
{
    public interface ICheckoutApi : ICheckoutApmApi, ICheckoutApiClient
    {
        ITokensClient TokensClient();

        ICustomersClient CustomersClient();

        ISourcesClient SourcesClient();

        IPaymentsClient PaymentsClient();

        IInstrumentsClient InstrumentsClient();

        IDisputesClient DisputesClient();

        IWebhooksClient WebhooksClient();

        IEventsClient EventsClient();

        IRiskClient RiskClient();

        IPaymentLinksClient PaymentLinksClient();

        IReconciliationClient ReconciliationClient();

        IHostedPaymentsClient HostedPaymentsClient();
    }
}