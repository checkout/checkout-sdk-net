using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Agentic
{
    public class AgenticClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AgenticClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateAgentic_Should_Call_ApiClient_Post()
        {
            CreateAgenticRequest request = new CreateAgenticRequest();
            CreateAgenticResponse response = new CreateAgenticResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Post<CreateAgenticResponse>(
                    "agentic",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            CreateAgenticResponse result = await client.CreateAgentic(request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgentic_Should_Call_ApiClient_Get()
        {
            string agenticId = "agentic_id";
            GetAgenticResponse response = new GetAgenticResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Get<GetAgenticResponse>(
                    "agentic/agentic_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            GetAgenticResponse result = await client.GetAgentic(agenticId);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task UpdateAgentic_Should_Call_ApiClient_Patch()
        {
            string agenticId = "agentic_id";
            UpdateAgenticRequest request = new UpdateAgenticRequest();
            UpdateAgenticResponse response = new UpdateAgenticResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Patch<UpdateAgenticResponse>(
                    "agentic/agentic_id",
                    _authorization,
                    request,
                    CancellationToken.None
                    , null))
                .ReturnsAsync(response);
            
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            UpdateAgenticResponse result = await client.UpdateAgentic(agenticId, request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task DeleteAgentic_Should_Call_ApiClient_Delete()
        {
            string agenticId = "agentic_id";
            DeleteAgenticResponse response = new DeleteAgenticResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Delete<DeleteAgenticResponse>(
                    "agentic/agentic_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            DeleteAgenticResponse result = await client.DeleteAgentic(agenticId);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgentics_Should_Call_ApiClient_Query()
        {
            GetAgenticsRequest request = new GetAgenticsRequest();
            GetAgenticsResponse response = new GetAgenticsResponse();
            _apiClient.Setup(apiClient => 
                    apiClient.Query<GetAgenticsResponse>(
                    "agentic",
                    _authorization,
                    request,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            GetAgenticsResponse result = await client.GetAgentics(request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
    }
}