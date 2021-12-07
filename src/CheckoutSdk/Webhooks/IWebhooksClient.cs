using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Webhooks
{
    public interface IWebhooksClient
    {
        Task<IList<WebhookResponse>> RetrieveWebhooks(CancellationToken cancellationToken = default);

        Task<WebhookResponse> RegisterWebhook(WebhookRequest webhookRequest, string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<WebhookResponse> RetrieveWebhook(string webhookId, CancellationToken cancellationToken = default);

        Task<WebhookResponse> UpdateWebhook(string webhookId, WebhookRequest webhookRequest,
            CancellationToken cancellationToken = default);

        Task<WebhookResponse> PatchWebhook(string webhookId, WebhookRequest webhookRequest,
            CancellationToken cancellationToken = default);

        Task<object> RemoveWebhook(string webhookId, CancellationToken cancellationToken = default);
    }
}