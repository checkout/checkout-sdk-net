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