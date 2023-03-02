using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Previous.Sepa
{
    public class SepaClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public SepaClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldGetMandate()
        {
            var mandateResponse = new MandateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<MandateResponse>("sepa/mandates/id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => mandateResponse);

            var sepaClient = new SepaClient(_apiClient.Object, _configuration.Object);

            var response = await sepaClient.GetMandate("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCancelMandate()
        {
            var sepaResource = new SepaResource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<SepaResource>("sepa/mandates/id/cancel", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => sepaResource);

            var sepaClient = new SepaClient(_apiClient.Object, _configuration.Object);

            var response = await sepaClient.CancelMandate("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetMandateViaPpro()
        {
            var mandateResponse = new MandateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<MandateResponse>("apms/ppro/sepa/mandates/id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => mandateResponse);

            var sepaClient = new SepaClient(_apiClient.Object, _configuration.Object);

            var response = await sepaClient.GetMandateViaPpro("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCancelMandateViaPPro()
        {
            var sepaResource = new SepaResource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<SepaResource>("apms/ppro/sepa/mandates/id/cancel", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => sepaResource);

            var sepaClient = new SepaClient(_apiClient.Object, _configuration.Object);

            var response = await sepaClient.CancelMandateViaPpro("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}