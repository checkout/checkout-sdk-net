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
        /// <summary>
        /// Creates single-use tokens from card details, wallet payloads or Apple Pay and Google Pay data.
        /// </summary>
        ITokensClient TokensClient();

        /// <summary>
        /// Creates, retrieves, updates and deletes customers and their stored instruments.
        /// </summary>
        ICustomersClient CustomersClient();

        /// <summary>
        /// Creates and manages reusable payment sources.
        /// </summary>
        ISourcesClient SourcesClient();

        /// <summary>
        /// Requests payments and payouts, and retrieves, captures, refunds and voids them.
        /// </summary>
        IPaymentsClient PaymentsClient();

        /// <summary>
        /// Stores, retrieves, updates and deletes payment instruments.
        /// </summary>
        IInstrumentsClient InstrumentsClient();

        /// <summary>
        /// Retrieves disputes and submits evidence against them.
        /// </summary>
        IDisputesClient DisputesClient();

        /// <summary>
        /// Registers and manages webhook endpoints and their event subscriptions.
        /// </summary>
        IWebhooksClient WebhooksClient();

        /// <summary>
        /// Retrieves events and their notifications, and retries webhook deliveries.
        /// </summary>
        IEventsClient EventsClient();

        /// <summary>
        /// Runs pre-authentication and pre-capture risk assessments.
        /// </summary>
        IRiskClient RiskClient();

        /// <summary>
        /// Creates and retrieves payment links.
        /// </summary>
        IPaymentLinksClient PaymentLinksClient();

        /// <summary>
        /// Retrieves payment and statement reconciliation reports.
        /// </summary>
        IReconciliationClient ReconciliationClient();

        /// <summary>
        /// Creates and retrieves hosted payment pages.
        /// </summary>
        IHostedPaymentsClient HostedPaymentsClient();
    }
}