using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Default implementation of <see cref="IWebhooksClient"/>.
    /// </summary>
    public class WebhooksClient : IWebhooksClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;
        private const string path = "webhooks";

        public WebhooksClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<WebhooksResponse> RetrieveWebhooksAsync(CancellationToken cancellationToken = default(CancellationToken))
        {            
            return _apiClient.GetAsync<WebhooksResponse>(path, _credentials, cancellationToken);
        }

        public Task<WebhookResponse> RegisterWebhookAsync(RegisterWebhookRequest webhookRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<WebhookResponse>(path, _credentials, cancellationToken, webhookRequest);
        }

        public Task<WebhookResponse> RetrieveWebhookAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<WebhookResponse>($"{path}/{id}", _credentials, cancellationToken);
        }

        public Task<WebhookResponse> UpdateWebhookAsync (string id, UpdateWebhookRequest webhookRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PutAsync<WebhookResponse>($"{path}/{id}", _credentials, cancellationToken, webhookRequest);
        }

        public Task<WebhookResponse> PartiallyUpdateWebhookAsync(string id, PartialUpdateWebhookRequest webhookRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PatchAsync<WebhookResponse>($"{path}/{id}", _credentials, cancellationToken, webhookRequest);
        }

        public Task<Type> RemoveWebhookAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.DeleteAsync<Type>($"{path}/{id}", _credentials, cancellationToken);
        }
    }
}
