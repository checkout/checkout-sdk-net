using Checkout;
using Checkout.Forward.Requests;
using Checkout.Forward.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Forward
{
    public class ForwardClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ForwardClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task ForwardAnApiRequest_Should_Call_ApiClient_Post()
        {
            ForwardRequest request = new ForwardRequest();
            ForwardAnApiResponse response = new ForwardAnApiResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Post<ForwardAnApiResponse>(
                    "forward",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            ForwardAnApiResponse result = await client.ForwardAnApiRequest(request);

            result.ShouldNotBeNull(); 
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetForwardRequest_Should_Call_ApiClient_Get()
        {
            string forwardId = "forward_id";
            GetForwardResponse response = new GetForwardResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Get<GetForwardResponse>(
                    "forward/forward_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            GetForwardResponse result = await client.GetForwardRequest(forwardId);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task CreateSecret_Should_Call_ApiClient_Post()
        {
            var request = new SecretRequest { Name = "secret_name", Value = "plaintext", EntityId = "ent_123" };
            var response = new SecretResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<SecretResponse>(
                        "forward/secrets",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            SecretResponse result = await client.CreateSecret(request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task ListSecrets_Should_Call_ApiClient_Get()
        {
            var response = new ItemsResponse<SecretResponse>();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<SecretResponse>>(
                        "forward/secrets",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            ItemsResponse<SecretResponse> result = await client.ListSecrets();

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task UpdateSecret_Should_Call_ApiClient_Patch()
        {
            var request = new SecretRequest { Value = "new_value", EntityId = "ent_456" };
            var response = new SecretResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Patch<SecretResponse>(
                        "forward/secrets/secret_name",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            SecretResponse result = await client.UpdateSecret("secret_name", request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task DeleteSecret_Should_Call_ApiClient_Delete()
        {
            var response = new EmptyResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>(
                        "forward/secrets/secret_name",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IForwardClient client = new ForwardClient(_apiClient.Object, _configuration.Object);

            EmptyResponse result = await client.DeleteSecret("secret_name");

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
    }
}