using Checkout.OnboardingSimulator.Entities;
using Checkout.OnboardingSimulator.Requests;
using Checkout.OnboardingSimulator.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.OnboardingSimulator
{
    public class OnboardingSimulatorIntegrationTest : SandboxTestFixture
    {
        public OnboardingSimulatorIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "Sandbox-only. Requires an existing sub-entity ID")]
        private async Task ShouldListAvailableRequirements()
        {
            var response = await DefaultApi.OnboardingSimulatorClient().ListAvailableRequirements();

            ValidateAvailableRequirementsResponse(response);
        }

        [Fact(Skip = "Sandbox-only. Requires an existing sub-entity ID")]
        private async Task ShouldListScenarios()
        {
            var response = await DefaultApi.OnboardingSimulatorClient().ListScenarios();

            ValidateScenariosResponse(response);
        }

        [Fact(Skip = "Sandbox-only. Requires an existing sub-entity ID")]
        private async Task ShouldSetRequirementsDue()
        {
            var entityId = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_ENTITY_ID");
            var request = CreateSetRequirementsDueRequest();

            var response = await DefaultApi.OnboardingSimulatorClient().SetRequirementsDue(entityId, request);

            ValidateSetRequirementsDueResponse(response, entityId);
        }

        [Fact(Skip = "Sandbox-only. Requires an existing sub-entity ID")]
        private async Task ShouldRunScenario()
        {
            var entityId = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_ENTITY_ID");
            const string scenarioId = "go_active";

            var response = await DefaultApi.OnboardingSimulatorClient().RunScenario(entityId, scenarioId);

            ValidateRunScenarioResponse(response, entityId, scenarioId);
        }

        [Fact(Skip = "Sandbox-only. Requires an existing sub-entity ID")]
        private async Task ShouldSetEntityStatus()
        {
            var entityId = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_ENTITY_ID");
            var request = new SimulatorSetStatusRequest { Status = SimulatorEntityStatus.Active };

            var response = await DefaultApi.OnboardingSimulatorClient().SetEntityStatus(entityId, request);

            ValidateSetStatusResponse(response, entityId);
        }

        private static SimulatorSetRequirementsDueRequest CreateSetRequirementsDueRequest()
        {
            return new SimulatorSetRequirementsDueRequest
            {
                Fields = new List<string> { "individual.identification.document" }
            };
        }

        private static void ValidateAvailableRequirementsResponse(ItemsResponse<SimulatorAvailableRequirement> response)
        {
            response.ShouldNotBeNull();
            response.Items.ShouldNotBeNull();
            foreach (var item in response.Items)
            {
                item.Field.ShouldNotBeNullOrEmpty();
                item.Type.ShouldNotBeNullOrEmpty();
            }
        }

        private static void ValidateScenariosResponse(ItemsResponse<SimulatorScenario> response)
        {
            response.ShouldNotBeNull();
            response.Items.ShouldNotBeNull();
            foreach (var scenario in response.Items)
            {
                scenario.Id.ShouldNotBeNullOrEmpty();
                scenario.Name.ShouldNotBeNullOrEmpty();
            }
        }

        private static void ValidateSetRequirementsDueResponse(SimulatorSetRequirementsDueResponse response, string entityId)
        {
            response.ShouldNotBeNull();
            response.EntityId.ShouldBe(entityId);
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
            response.RequirementsDue.ShouldNotBeNull();
        }

        private static void ValidateRunScenarioResponse(SimulatorRunScenarioResponse response, string entityId, string scenarioId)
        {
            response.ShouldNotBeNull();
            response.EntityId.ShouldBe(entityId);
            response.ScenarioId.ShouldBe(scenarioId);
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateSetStatusResponse(SimulatorSetStatusResponse response, string entityId)
        {
            response.ShouldNotBeNull();
            response.EntityId.ShouldBe(entityId);
            response.CurrentStatus.ShouldNotBeNullOrEmpty();
        }
    }
}
