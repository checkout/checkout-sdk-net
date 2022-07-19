using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using Moq;
using Shouldly;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Accounts
{
    public class AccountsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Four, ValidDefaultSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Four);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<IApiClient> _apiFilesClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly AccountsClient _accountsClient;

        public AccountsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory);
            _accountsClient =
                new AccountsClient(_apiClient.Object, _apiFilesClient.Object, configuration.Object);
        }

        [Fact]
        public async Task ShouldCreateEntity()
        {
            var onboardEntityResponse = new OnboardEntityResponse {Id = "Id"};

            _apiClient.Setup(x => x.Post<OnboardEntityResponse>("accounts/entities", It.IsAny<SdkAuthorization>(),
                    It.IsAny<object>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(onboardEntityResponse);

            var response = await _accountsClient.CreateEntity(new OnboardEntityRequest());

            response.ShouldNotBeNull();
            response.ShouldBe(onboardEntityResponse);
        }

        [Fact]
        private async Task ShouldGetEntity()
        {
            var responseObject = new OnboardEntityDetailsResponse {Id = "entity_id"};

            _apiClient
                .Setup(x =>
                    x.Get<OnboardEntityDetailsResponse>(
                        "accounts/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseObject);

            var response = await _accountsClient.GetEntity(responseObject.Id);

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldUpdateEntity()
        {
            var responseObject = new OnboardEntityResponse {Id = "entity_id", Reference = "A"};

            _apiClient
                .Setup(x =>
                    x.Put<OnboardEntityResponse>(
                        "accounts/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(responseObject);

            var response = await _accountsClient.UpdateEntity(
                responseObject.Id,
                new OnboardEntityRequest {Reference = "A"});

            response.ShouldNotBeNull();
            response.Id.ShouldBe(responseObject.Id);
            response.Reference.ShouldBe(responseObject.Reference);
        }

        [Fact]
        private async Task ShouldCreatePaymentInstrument()
        {
            _apiClient
                .Setup(x =>
                    x.Post<EmptyResponse>(
                        "accounts/entities/entity_id/instruments",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(() => new EmptyResponse());

            var response = await _accountsClient.CreatePaymentInstrument(
                "entity_id",
                new AccountsPaymentInstrument {AccountNumber = "324445"});

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldSubmitFile()
        {
            //Arrange
            var responseObject = new IdResponse {Id = "Id"};

            _apiFilesClient
                .Setup(x =>
                    x.Post<IdResponse>(
                        It.IsAny<string>(),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<MultipartFormDataContent>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseObject);

            var response =
                await _accountsClient.SubmitFile(new AccountsFileRequest
                {
                    File = "./Resources/checkout.jpeg",
                    ContentType = null,
                    Purpose = AccountsFilePurpose.Identification
                });

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldUpdatePayoutSchedule()
        {
            var responseAsync = new EmptyResponse();

            _apiClient
                .Setup(x =>
                    x.Put<EmptyResponse>(
                        "accounts/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _accountsClient.UpdatePayoutSchedule("entity_id", Currency.AED, new UpdateScheduleRequest());

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrievePayoutSchedule()
        {
            var responseAsync = new GetScheduleResponse();

            _apiClient
                .Setup(x =>
                    x.Get<GetScheduleResponse>(
                        "accounts/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseAsync);

            var response = await _accountsClient.RetrievePayoutSchedule(
                "entity_id");

            response.ShouldNotBeNull();
        }
    }
}