using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Instruments.Previous
{
    public class InstrumentsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public InstrumentsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldGetInstrument()
        {
            var instrumentResponse = new RetrieveInstrumentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<RetrieveInstrumentResponse>("instruments/instrument_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => instrumentResponse);

            IInstrumentsClient client = new InstrumentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.Get("instrument_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(instrumentResponse);
        }

        [Fact]
        private async Task ShouldCreateInstrument()
        {
            var createInstrumentRequest = new CreateInstrumentRequest();
            var createInstrumentResponse = new CreateInstrumentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CreateInstrumentResponse>("instruments", _authorization, createInstrumentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => createInstrumentResponse);

            IInstrumentsClient client = new InstrumentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.Create(createInstrumentRequest);

            response.ShouldNotBeNull();
            response.ShouldBe(createInstrumentResponse);
        }

        [Fact]
        private async Task ShouldUpdateInstrument()
        {
            var updateInstrumentRequest = new UpdateInstrumentRequest();
            var updateInstrumentResponse = new UpdateInstrumentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<UpdateInstrumentResponse>("instruments/instrument_id", _authorization,
                        updateInstrumentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => updateInstrumentResponse);

            IInstrumentsClient client = new InstrumentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.Update("instrument_id", updateInstrumentRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(updateInstrumentResponse);
        }

        [Fact]
        private async Task ShouldDeleteInstrument()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>("instruments/instrument_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new EmptyResponse());

            IInstrumentsClient client = new InstrumentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.Delete("instrument_id");

            response.ShouldNotBeNull();
        }
    }
}