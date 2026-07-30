using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.AddressDocumentVerification.Responses;
using Checkout.Identities.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Identities.AddressDocumentVerification
{
    public class AddressDocumentVerificationClientTest : UnitTestFixture
    {
        private const string AddressDocumentVerificationsPath = "address-document-verifications";
        private const string AddressDocumentVerificationId = "adv_tkoi5db4hryu5cei5vwoabr7we";
        private const string AttemptId = "adva_tkoi5db4hryu5cei5vwoabr7we";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AddressDocumentVerificationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        private IAddressDocumentVerificationClient Client() =>
            new AddressDocumentVerificationClient(_apiClient.Object, _configuration.Object);

        [Fact]
        public async Task CreateAddressDocumentVerification_Should_Call_ApiClient_Post()
        {
            var request = new AddressDocumentVerificationRequest
            {
                ApplicantId = "aplt_tkoi5db4hryu5cei5vwoabr7we",
                UserJourneyId = "usj_tkoi5db4hryu5cei5vwoabr7we",
                DeclaredData = new DeclaredData { Name = "Hannah Bret" }
            };
            var response = new AddressDocumentVerificationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AddressDocumentVerificationResponse>(
                        AddressDocumentVerificationsPath, _authorization, request, CancellationToken.None, null))
                .ReturnsAsync(response);

            var result = await Client().CreateAddressDocumentVerification(request, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAddressDocumentVerification_Should_Call_ApiClient_Get()
        {
            var response = new AddressDocumentVerificationResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<AddressDocumentVerificationResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId,
                        _authorization, CancellationToken.None))
                .ReturnsAsync(response);

            var result = await Client().GetAddressDocumentVerification(AddressDocumentVerificationId, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task AnonymizeAddressDocumentVerification_Should_Call_ApiClient_Post()
        {
            var response = new AddressDocumentVerificationResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<AddressDocumentVerificationResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId + "/anonymize",
                        _authorization, (object)null, CancellationToken.None, null))
                .ReturnsAsync(response);

            var result = await Client().AnonymizeAddressDocumentVerification(AddressDocumentVerificationId, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task CreateAttempt_Should_Call_ApiClient_Post()
        {
            var request = new AddressDocumentVerificationAttemptRequest { Document = "base64-data" };
            var response = new AddressDocumentVerificationAttemptResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<AddressDocumentVerificationAttemptResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId + "/attempts",
                        _authorization, request, CancellationToken.None, null))
                .ReturnsAsync(response);

            var result = await Client().CreateAddressDocumentVerificationAttempt(
                AddressDocumentVerificationId, request, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAttempts_Should_Call_ApiClient_Get()
        {
            var response = new AddressDocumentVerificationAttemptsResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<AddressDocumentVerificationAttemptsResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId + "/attempts",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(response);

            var result = await Client().GetAddressDocumentVerificationAttempts(AddressDocumentVerificationId, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAttempt_Should_Call_ApiClient_Get()
        {
            var response = new AddressDocumentVerificationAttemptResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<AddressDocumentVerificationAttemptResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId + "/attempts/" + AttemptId,
                        _authorization, CancellationToken.None))
                .ReturnsAsync(response);

            var result = await Client().GetAddressDocumentVerificationAttempt(
                AddressDocumentVerificationId, AttemptId, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetReport_Should_Call_ApiClient_Get()
        {
            var response = new AddressDocumentVerificationReportResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<AddressDocumentVerificationReportResponse>(
                        AddressDocumentVerificationsPath + "/" + AddressDocumentVerificationId + "/pdf-report",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(response);

            var result = await Client().GetAddressDocumentVerificationReport(AddressDocumentVerificationId, CancellationToken.None);

            result.ShouldBeSameAs(response);
        }
    }
}
