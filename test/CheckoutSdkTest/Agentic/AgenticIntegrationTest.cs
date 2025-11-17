using System;
using Checkout.Agentic.Requests;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Agentic
{
    public class AgenticIntegrationTest : SandboxTestFixture
    {
        public AgenticIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldCreateAgentic()
        {
            var agenticRequest = CreateAgenticRequest();

            var agenticResponse = await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            agenticResponse.ShouldNotBeNull();
            agenticResponse.Id.ShouldNotBeNull();
            agenticResponse.Status.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldGetAgentic()
        {
            var agenticRequest = CreateAgenticRequest();

            var agenticResponse = await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            var getAgenticResponse = await DefaultApi.AgenticClient().GetAgentic(agenticResponse.Id);

            getAgenticResponse.ShouldNotBeNull();
            getAgenticResponse.Id.ShouldNotBeNull();
            getAgenticResponse.Name.ShouldNotBeNull();
            getAgenticResponse.Status.ShouldNotBeNull();
            getAgenticResponse.CreatedAt.ShouldNotBe(DateTime.MinValue);
            getAgenticResponse.Configuration.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldUpdateAgentic()
        {
            var agenticRequest = CreateAgenticRequest();
            var agenticResponse = await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            var updateRequest = new UpdateAgenticRequest
            {
                Name = "Updated Test Agentic",
                Description = "Updated description"
            };

            var updateResponse = await DefaultApi.AgenticClient().UpdateAgentic(agenticResponse.Id, updateRequest);

            updateResponse.ShouldNotBeNull();
            updateResponse.Id.ShouldBe(agenticResponse.Id);
            updateResponse.Status.ShouldNotBeNull();
            updateResponse.UpdatedAt.ShouldNotBe(DateTime.MinValue);
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldDeleteAgentic()
        {
            var agenticRequest = CreateAgenticRequest();
            var agenticResponse = await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            var deleteResponse = await DefaultApi.AgenticClient().DeleteAgentic(agenticResponse.Id);

            deleteResponse.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldGetAgentics()
        {
            var agenticRequest = CreateAgenticRequest();
            await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            var getAgenticsResponse = await DefaultApi.AgenticClient().GetAgentics();

            getAgenticsResponse.ShouldNotBeNull();
            getAgenticsResponse.Items.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid agentic configuration")]
        private async Task ShouldGetAgenticsWithFilters()
        {
            var agenticRequest = CreateAgenticRequest();
            await DefaultApi.AgenticClient().CreateAgentic(agenticRequest);

            var getRequest = new GetAgenticsRequest
            {
                Limit = 10
            };

            var getAgenticsResponse = await DefaultApi.AgenticClient().GetAgentics(getRequest);

            getAgenticsResponse.ShouldNotBeNull();
            getAgenticsResponse.Items.ShouldNotBeNull();
        }

        private CreateAgenticRequest CreateAgenticRequest()
        {
            return new CreateAgenticRequest
            {
                Name = "Test Agentic",
                Description = "Test agentic for integration testing",
                Configuration = new AgenticConfiguration
                {
                    AiModel = "gpt-4",
                    MaxAutonomousActions = 10,
                    EnableAutonomousPayments = true,
                    RiskThreshold = 0.5m
                }
            };
        }
    }
}