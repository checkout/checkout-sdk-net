using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Transfers
{
    public class TransfersClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidPreviousSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly TransfersClient _transfersClient;

        public TransfersClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory, null);
            _transfersClient =
                new TransfersClient(_apiClient.Object, configuration.Object);
        }


        [Fact]
        public async Task ShouldInitiateTransferOfFunds()
        {
            var createTransferRequest = new CreateTransferRequest {Reference = "Reference"};
            var createTransferResponse = new CreateTransferResponse();

            _apiClient.Setup(x => x.Post<CreateTransferResponse>("transfers", It.IsAny<SdkAuthorization>(),
                    createTransferRequest, It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(createTransferResponse);

            var response = await _transfersClient.InitiateTransferOfFunds(createTransferRequest);

            response.ShouldNotBeNull();
            response.ShouldBe(createTransferResponse);
        }

        [Fact]
        public async Task ShouldRetrieveATransfer()
        {
            var transferDetailsResponse = new TransferDetailsResponse();

            _apiClient.Setup(x => x.Get<TransferDetailsResponse>("transfers/transfer_id", It.IsAny<SdkAuthorization>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(transferDetailsResponse);

            var response = await _transfersClient.RetrieveATransfer("transfer_id");

            response.ShouldNotBeNull();
            response.ShouldBe(transferDetailsResponse);
        }
    }
} 