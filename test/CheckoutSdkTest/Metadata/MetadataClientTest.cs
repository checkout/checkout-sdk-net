using Checkout.Metadata.Card;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Metadata
{
    public class MetadataClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public MetadataClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestCardMetadata()
        {
            var request = new CardMetadataRequest();
            var expectedResponse = new CardMetadataResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardMetadataResponse>("metadata/card", _authorization,
                        request, CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IMetadataClient client = new MetadataClient(_apiClient.Object, _configuration.Object);

            var response = await client.RequestCardMetadata(request);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        private async Task ShouldRequestCardMetadataWithCancellationToken()
        {
            var request = new CardMetadataRequest();
            var expectedResponse = new CardMetadataResponse();
            var cancellationToken = new CancellationToken();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardMetadataResponse>("metadata/card", _authorization,
                        request, cancellationToken, null))
                .ReturnsAsync(() => expectedResponse);

            IMetadataClient client = new MetadataClient(_apiClient.Object, _configuration.Object);

            var response = await client.RequestCardMetadata(request, cancellationToken);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        private async Task ShouldReturnNullWhenApiReturnsUnexpectedType()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>("metadata/card", _authorization, null, CancellationToken.None, null))
                .ReturnsAsync(() => new HttpMetadata());

            IMetadataClient client = new MetadataClient(_apiClient.Object, _configuration.Object);

            var response = await client.RequestCardMetadata(new CardMetadataRequest());

            response.ShouldBeNull();
        }
    }
}
