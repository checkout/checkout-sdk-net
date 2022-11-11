using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Metadata.Card
{
    public class MetadataCardClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public MetadataCardClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestMetadataCardBin()
        {
            CardMetadataRequest metadataCardRequest = new CardMetadataRequest();
            CardMetadataResponse cardMetadataResponse = new CardMetadataResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardMetadataResponse>("metadata/card", _authorization,
                        metadataCardRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardMetadataResponse);

            IMetadataClient client =
                new MetadataClient(_apiClient.Object, _configuration.Object);

            CardMetadataResponse response = await client.RequestCardMetadata(metadataCardRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardMetadataResponse);
        }

        [Fact]
        private async Task ShouldRequestMetadataCardError()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>("metadata/card", _authorization, null, CancellationToken.None, null))
                .ReturnsAsync(() => new HttpMetadata());

            IMetadataClient client = new MetadataClient(_apiClient.Object, _configuration.Object);

            CardMetadataResponse response = await client.RequestCardMetadata(new CardMetadataRequest());
            
            response.ShouldBeNull();
        }
    }
}