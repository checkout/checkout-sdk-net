using Checkout.OnboardingSimulator.Entities;
using Checkout.OnboardingSimulator.Requests;
using Checkout.OnboardingSimulator.Responses;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.OnboardingSimulator
{
    public class OnboardingSimulatorClientTest : UnitTestFixture
    {
        private const string EntityId = "ent_w4jelhppmfiufdnatam37wrfc4";
        private const string ScenarioId = "go_active";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public OnboardingSimulatorClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task SetRequirementsDue_Should_Call_ApiClient_Post()
        {
            var request = CreateSetRequirementsDueRequest();
            var response = CreateSetRequirementsDueResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<SimulatorSetRequirementsDueResponse>(
                        $"simulate/entities/{EntityId}/requirements-due",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            var result = await client.SetRequirementsDue(EntityId, request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateSetRequirementsDueResponse(result);
        }

        [Fact]
        public async Task RunScenario_Should_Call_ApiClient_Post()
        {
            var response = CreateRunScenarioResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<SimulatorRunScenarioResponse>(
                        $"simulate/entities/{EntityId}/scenarios/{ScenarioId}",
                        _authorization,
                        (object)null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            var result = await client.RunScenario(EntityId, ScenarioId);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateRunScenarioResponse(result);
        }

        [Fact]
        public async Task SetEntityStatus_Should_Call_ApiClient_Post()
        {
            var request = CreateSetStatusRequest();
            var response = CreateSetStatusResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Post<SimulatorSetStatusResponse>(
                        $"simulate/entities/{EntityId}/status",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            var result = await client.SetEntityStatus(EntityId, request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateSetStatusResponse(result);
        }

        [Fact]
        public async Task ListAvailableRequirements_Should_Call_ApiClient_Get()
        {
            var response = CreateAvailableRequirementsResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<SimulatorAvailableRequirement>>(
                        "simulate/requirements-due",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            var result = await client.ListAvailableRequirements();

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            result.Items.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ListScenarios_Should_Call_ApiClient_Get()
        {
            var response = CreateScenariosResponse();
            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<SimulatorScenario>>(
                        "simulate/scenarios",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            var result = await client.ListScenarios();

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            result.Items.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task SetRequirementsDue_Should_Throw_When_EntityId_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.SetRequirementsDue(null, CreateSetRequirementsDueRequest()));
        }

        [Fact]
        public async Task SetRequirementsDue_Should_Throw_When_Request_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.SetRequirementsDue(EntityId, null));
        }

        [Fact]
        public async Task RunScenario_Should_Throw_When_EntityId_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RunScenario(null, ScenarioId));
        }

        [Fact]
        public async Task RunScenario_Should_Throw_When_ScenarioId_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RunScenario(EntityId, null));
        }

        [Fact]
        public async Task SetEntityStatus_Should_Throw_When_EntityId_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.SetEntityStatus(null, CreateSetStatusRequest()));
        }

        [Fact]
        public async Task SetEntityStatus_Should_Throw_When_Request_Is_Null()
        {
            IOnboardingSimulatorClient client = new OnboardingSimulatorClient(_apiClient.Object, _configuration.Object);

            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.SetEntityStatus(EntityId, null));
        }

        private static SimulatorSetRequirementsDueRequest CreateSetRequirementsDueRequest()
        {
            return new SimulatorSetRequirementsDueRequest
            {
                Fields = new List<string> { "individual.identification.document" }
            };
        }

        private static SimulatorSetStatusRequest CreateSetStatusRequest()
        {
            return new SimulatorSetStatusRequest { Status = SimulatorEntityStatus.Active };
        }

        private static SimulatorSetRequirementsDueResponse CreateSetRequirementsDueResponse()
        {
            return new SimulatorSetRequirementsDueResponse
            {
                EntityId = EntityId,
                PreviousStatus = "Active",
                CurrentStatus = "requirements_due",
                RequirementsDue = new List<string> { "individual.identification.document" }
            };
        }

        private static SimulatorRunScenarioResponse CreateRunScenarioResponse()
        {
            return new SimulatorRunScenarioResponse
            {
                EntityId = EntityId,
                ScenarioId = ScenarioId,
                ScenarioName = "Go Active",
                PreviousStatus = "RequirementsDue",
                CurrentStatus = "Active",
                RequirementsDue = new List<string>()
            };
        }

        private static SimulatorSetStatusResponse CreateSetStatusResponse()
        {
            return new SimulatorSetStatusResponse
            {
                EntityId = EntityId,
                PreviousStatus = "Pending",
                CurrentStatus = "Active"
            };
        }

        private static ItemsResponse<SimulatorAvailableRequirement> CreateAvailableRequirementsResponse()
        {
            return new ItemsResponse<SimulatorAvailableRequirement>
            {
                Items = new List<SimulatorAvailableRequirement>
                {
                    new SimulatorAvailableRequirement { Field = "individual.identification.document", Type = "string" }
                }
            };
        }

        private static ItemsResponse<SimulatorScenario> CreateScenariosResponse()
        {
            return new ItemsResponse<SimulatorScenario>
            {
                Items = new List<SimulatorScenario>
                {
                    new SimulatorScenario
                    {
                        Id = ScenarioId,
                        Name = "Go Active",
                        Description = "Transitions the entity to active status.",
                        Action = "set_status",
                        Status = "active"
                    }
                }
            };
        }

        private static void ValidateSetRequirementsDueResponse(SimulatorSetRequirementsDueResponse response)
        {
            response.EntityId.ShouldBe(EntityId);
            response.PreviousStatus.ShouldNotBeNullOrEmpty();
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
            response.RequirementsDue.ShouldNotBeEmpty();
        }

        private static void ValidateRunScenarioResponse(SimulatorRunScenarioResponse response)
        {
            response.EntityId.ShouldBe(EntityId);
            response.ScenarioId.ShouldBe(ScenarioId);
            response.ScenarioName.ShouldNotBeNullOrEmpty();
            response.PreviousStatus.ShouldNotBeNullOrEmpty();
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateSetStatusResponse(SimulatorSetStatusResponse response)
        {
            response.EntityId.ShouldBe(EntityId);
            response.PreviousStatus.ShouldNotBeNullOrEmpty();
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
        }
    }
}
