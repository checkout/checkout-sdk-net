using Checkout.Common;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Reports
{
    public class ReportsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ReportsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }
        
        [Fact]
        private async Task ShouldGetAllReports()
        {
            var request = new ReportsQuery();
            var responseAsync = new ReportsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ReportsResponse>("reports", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IReportsClient client = new ReportsClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetAllReports(request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetReportDetails()
        {
            const string reportId = "rpt_1234";
            var responseAsync = new ReportDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ReportDetailsResponse>($"reports/{reportId}", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IReportsClient client = new ReportsClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetReportDetails(reportId);

            response.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldGetReportFile()
        {
            const string reportId = "rpt_1234";
            const string fileId = "file_1234";
            var responseAsync = new ContentsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ContentsResponse>($"reports/{reportId}/files/{fileId}", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IReportsClient client = new ReportsClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetReportFile(reportId, fileId);

            response.ShouldNotBeNull();
        }
    }
}
