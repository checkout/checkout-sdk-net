using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Webhooks.Previous
{
    public class WebhookClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public WebhookClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRetrieveWebhooks()
        {
            var webhookResponses = new ItemsResponse<WebhookResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<WebhookResponse>>("webhooks", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => webhookResponses);

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveWebhooks();

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(webhookResponses);
        }

        [Fact]
        private async Task ShouldRegisterWebhook()
        {
            var webhookRequest = new WebhookRequest();
            var webhookResponse = new WebhookResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<WebhookResponse>("webhooks", _authorization, webhookRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => webhookResponse);

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.RegisterWebhook(webhookRequest);

            response.ShouldNotBeNull();
            response.ShouldBe(webhookResponse);
        }

        [Fact]
        private async Task ShouldRetrieveWebhook()
        {
            var webhookResponse = new WebhookResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<WebhookResponse>("webhooks/wh_kve4kqtq3ueezaxriev666j4ky", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => webhookResponse);

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveWebhook("wh_kve4kqtq3ueezaxriev666j4ky");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(webhookResponse);
        }

        [Fact]
        private async Task ShouldUpdateWebhook()
        {
            var webhookRequest = new WebhookRequest();
            var webhookResponse = new WebhookResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<WebhookResponse>("webhooks/wh_kve4kqtq3ueezaxriev666j4ky", _authorization,
                        webhookRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => webhookResponse);

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.UpdateWebhook("wh_kve4kqtq3ueezaxriev666j4ky", webhookRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(webhookResponse);
        }

        [Fact]
        private async Task ShouldPatchWebhook()
        {
            var webhookRequest = new WebhookRequest();
            var webhookResponse = new WebhookResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<WebhookResponse>("webhooks/wh_kve4kqtq3ueezaxriev666j4ky", _authorization,
                        webhookRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => webhookResponse);

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.PatchWebhook("wh_kve4kqtq3ueezaxriev666j4ky", webhookRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(webhookResponse);
        }

        [Fact]
        private async Task ShouldRemoveWebhook()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>("webhooks/wh_kve4kqtq3ueezaxriev666j4ky", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new EmptyResponse());

            IWebhooksClient client = new WebhooksClient(_apiClient.Object, _configuration.Object);

            var response = await client.RemoveWebhook("wh_kve4kqtq3ueezaxriev666j4ky");

            response.ShouldNotBeNull();
        }
    }
}