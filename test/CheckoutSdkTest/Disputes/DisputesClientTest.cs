using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Disputes
{
    public class DisputesClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public DisputesClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldQueryDispute()
        {
            var request = new DisputesQueryFilter();
            var responseAsync = new DisputesQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<DisputesQueryResponse>("disputes", _authorization, request,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.Query(request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetDisputeDetails()
        {
            const string disputeId = "dsp_s5151531";
            var responseAsync = new DisputeDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<DisputeDetailsResponse>($"disputes/{disputeId}", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetDisputeDetails(disputeId);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldAcceptDispute()
        {
            const string disputeId = "dsp_s5151531";

            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>($"disputes/{disputeId}/accept", _authorization, null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => new EmptyResponse());

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.Accept(disputeId);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldPutEvidenceDispute()
        {
            const string disputeId = "dsp_s5151531";
            var request = new DisputeEvidenceRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<EmptyResponse>($"disputes/{disputeId}/evidence", _authorization, request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => new EmptyResponse());

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.PutEvidence(disputeId, request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetEvidence()
        {
            const string disputeId = "dsp_s5151531";
            var responseAsync = new DisputeEvidenceResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<DisputeEvidenceResponse>($"disputes/{disputeId}/evidence", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => responseAsync);

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetEvidence(disputeId);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldSubmitEvidence()
        {
            const string disputeId = "dsp_s5151531";

            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>($"disputes/{disputeId}/evidence", _authorization, null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => new EmptyResponse());

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.SubmitEvidence(disputeId);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetDisputeSchemeFiles()
        {
            const string disputeId = "dispute_id";

            _apiClient.Setup(apiClient =>
                    apiClient.Get<SchemeFileResponse>($"disputes/{disputeId}/schemefiles", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new SchemeFileResponse());

            IDisputesClient client = new DisputesClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetDisputeSchemeFiles(disputeId);

            response.ShouldNotBeNull();
        }
    }
}