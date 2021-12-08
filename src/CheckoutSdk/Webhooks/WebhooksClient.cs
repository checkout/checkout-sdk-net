using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Webhooks
{
    public class WebhooksClient : AbstractClient, IWebhooksClient
    {
        private const string WebhooksPath = "webhooks";

        public WebhooksClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<IList<WebhookResponse>> RetrieveWebhooks(CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<IList<WebhookResponse>>(WebhooksPath, SdkAuthorization(), cancellationToken);
        }

        public Task<WebhookResponse> RegisterWebhook(WebhookRequest webhookRequest, string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("webhookRequest", webhookRequest);
            return ApiClient.Post<WebhookResponse>(WebhooksPath, SdkAuthorization(), webhookRequest, cancellationToken,
                idempotencyKey);
        }

        public Task<WebhookResponse> RetrieveWebhook(string webhookId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("webhookId", webhookId);
            return ApiClient.Get<WebhookResponse>(BuildPath(WebhooksPath, webhookId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<WebhookResponse> UpdateWebhook(string webhookId, WebhookRequest webhookRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("webhookId", webhookId, "webhookRequest", webhookRequest);
            return ApiClient.Put<WebhookResponse>(BuildPath(WebhooksPath, webhookId), SdkAuthorization(),
                webhookRequest,
                cancellationToken);
        }

        public Task<WebhookResponse> PatchWebhook(string webhookId, WebhookRequest webhookRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("webhookId", webhookId, "webhookRequest", webhookRequest);
            return ApiClient.Patch<WebhookResponse>(BuildPath(WebhooksPath, webhookId), SdkAuthorization(),
                webhookRequest,
                cancellationToken);
        }

        public Task<object> RemoveWebhook(string webhookId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("webhookId", webhookId);
            return ApiClient.Delete<object>(BuildPath(WebhooksPath, webhookId), SdkAuthorization(), cancellationToken);
        }
    }
}