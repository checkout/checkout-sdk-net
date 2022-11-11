using Moq;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.MetaData.Card
{
    public class MetaDataCardClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public MetaDataCardClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestMetaDataCard()
        {
            MetaDataCardRequest metaDataCardRequest = new MetaDataCardRequest();
            MetaDataCardResponse metaDataCardResponse = new MetaDataCardResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<MetaDataCardResponse>("metadata/card", _authorization,
                        metaDataCardRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => metaDataCardResponse);

            IMetaDataCardClient client =
                new MetaDataCardClient(_apiClient.Object, _configuration.Object);

            MetaDataCardResponse response = await client.RequestCardMetaData(metaDataCardRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(metaDataCardResponse);
        }

        [Fact]
        private async Task ShouldRequestMetaDataCardError()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>("metadata/card", _authorization, null, CancellationToken.None, null))
                .ReturnsAsync(() => new HttpMetadata());

            IMetaDataCardClient client = new MetaDataCardClient(_apiClient.Object, _configuration.Object);

            MetaDataCardResponse response = await client.RequestCardMetaData(new MetaDataCardRequest());
            
            response.ShouldBeNull();
        }
    }
}