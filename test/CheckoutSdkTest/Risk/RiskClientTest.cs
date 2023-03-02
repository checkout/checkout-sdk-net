using Checkout.Risk.PreAuthentication;
using Checkout.Risk.PreCapture;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Risk
{
    public class RiskClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public RiskClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }


        [Fact]
        private async Task ShouldRequestPreAuthenticationRiskScan()
        {
            var request = new PreAuthenticationAssessmentRequest();
            var responseAsync = new PreAuthenticationAssessmentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PreAuthenticationAssessmentResponse>("risk/assessments/pre-authentication",
                        _authorization, request,
                        CancellationToken.None, null))
                .ReturnsAsync(() => responseAsync);

            IRiskClient client = new RiskClient(_apiClient.Object, _configuration.Object);

            var response = await client.RequestPreAuthenticationRiskScan(request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRequestPreCaptureRiskScan()
        {
            var request = new PreCaptureAssessmentRequest();
            var responseAsync = new PreCaptureAssessmentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PreCaptureAssessmentResponse>("risk/assessments/pre-capture", _authorization,
                        request,
                        CancellationToken.None, null))
                .ReturnsAsync(() => responseAsync);

            IRiskClient client = new RiskClient(_apiClient.Object, _configuration.Object);

            var response = await client.RequestPreCaptureRiskScan(request);

            response.ShouldNotBeNull();
        }
    }
}