using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Events;
using Checkout.Instruments;
using Checkout.Payments;
using Checkout.Risk;
using Checkout.Sources;
using Checkout.Tokens;
using Checkout.Webhooks;

namespace Checkout
{
    public interface ICheckoutApi : ICheckoutApmApi
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
    }
}