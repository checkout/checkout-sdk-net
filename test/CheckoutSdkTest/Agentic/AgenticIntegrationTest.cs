using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Agentic
{
    public class AgenticIntegrationTest : SandboxTestFixture
    {
        public AgenticIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldCreateAgentic()
        {
            // Arrange
            var createRequest = CreateValidAgenticRequest();

            // Act
            var createResponse = await DefaultApi.AgenticClient().CreateAgentic(createRequest);

            // Assert
            createResponse.ShouldNotBeNull();
            createResponse.Id.ShouldNotBeNull();
            createResponse.Name.ShouldBe(createRequest.Name);
            createResponse.Status.ShouldNotBeNull();
            createResponse.CreatedAt.ShouldNotBeNull();
            createResponse.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldGetAgentic()
        {
            // Arrange
            var createRequest = CreateValidAgenticRequest();
            var createResponse = await DefaultApi.AgenticClient().CreateAgentic(createRequest);

            // Act
            var getResponse = await DefaultApi.AgenticClient().GetAgentic(createResponse.Id);

            // Assert
            getResponse.ShouldNotBeNull();
            getResponse.Id.ShouldBe(createResponse.Id);
            getResponse.Name.ShouldBe(createRequest.Name);
            getResponse.Description.ShouldBe(createRequest.Description);
            getResponse.Status.ShouldNotBeNull();
            getResponse.IsActive.ShouldNotBeNull();
            getResponse.Configuration.ShouldNotBeNull();
            getResponse.Configuration.AiModel.ShouldBe(createRequest.Configuration.AiModel);
            getResponse.Configuration.MaxAutonomousActions.ShouldBe(createRequest.Configuration.MaxAutonomousActions);
            getResponse.CreatedAt.ShouldNotBeNull();
            getResponse.UpdatedAt.ShouldNotBeNull();
            getResponse.Statistics.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldUpdateAgentic()
        {
            // Arrange
            var createRequest = CreateValidAgenticRequest();
            var createResponse = await DefaultApi.AgenticClient().CreateAgentic(createRequest);

            var updateRequest = new UpdateAgenticRequest
            {
                Name = "Updated Agentic",
                Description = "Updated description for testing",
                Configuration = new AgenticConfiguration
                {
                    AiModel = "gpt-4-turbo",
                    MaxAutonomousActions = 150,
                    EnableAutonomousPayments = false,
                    RiskThreshold = 0.9m,
                    AllowedPaymentMethods = new List<string> { "card" }
                },
                IsActive = true
            };

            // Act
            var updateResponse = await DefaultApi.AgenticClient().UpdateAgentic(createResponse.Id, updateRequest);

            // Assert
            updateResponse.ShouldNotBeNull();
            updateResponse.Id.ShouldBe(createResponse.Id);
            updateResponse.Status.ShouldNotBeNull();
            updateResponse.UpdatedAt.ShouldNotBeNull();
            updateResponse.Links.ShouldNotBeNull();

            // Verify the update by getting the updated record
            var getResponse = await DefaultApi.AgenticClient().GetAgentic(createResponse.Id);
            getResponse.Name.ShouldBe(updateRequest.Name);
            getResponse.Description.ShouldBe(updateRequest.Description);
            getResponse.Configuration.AiModel.ShouldBe(updateRequest.Configuration.AiModel);
            getResponse.Configuration.MaxAutonomousActions.ShouldBe(updateRequest.Configuration.MaxAutonomousActions);
            getResponse.IsActive.ShouldBe(updateRequest.IsActive.Value);
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldGetAgentics()
        {
            // Arrange
            var createRequest1 = CreateValidAgenticRequest("Test Commerce 1");
            var createRequest2 = CreateValidAgenticRequest("Test Commerce 2");
            
            await DefaultApi.AgenticClient().CreateAgentic(createRequest1);
            await DefaultApi.AgenticClient().CreateAgentic(createRequest2);

            var listRequest = new GetAgenticsRequest
            {
                Skip = 0,
                Limit = 10,
                IsActive = true,
                SortBy = "created_at",
                SortDirection = "desc"
            };

            // Act
            var listResponse = await DefaultApi.AgenticClient().GetAgentics(listRequest);

            // Assert
            listResponse.ShouldNotBeNull();
            listResponse.Items.ShouldNotBeNull();
            listResponse.Items.Count.ShouldBeGreaterThan(0);
            listResponse.TotalItems.ShouldBeGreaterThanOrEqualTo(listResponse.Items.Count);
            listResponse.Count.ShouldBe(listResponse.Items.Count);
            listResponse.Skip.ShouldBe(0);
            listResponse.Limit.ShouldBe(10);

            // Check individual items
            foreach (var item in listResponse.Items)
            {
                item.Id.ShouldNotBeNull();
                item.Name.ShouldNotBeNull();
                item.Status.ShouldNotBeNull();
                item.IsActive.ShouldNotBeNull();
                item.AiModel.ShouldNotBeNull();
                item.CreatedAt.ShouldNotBeNull();
                item.UpdatedAt.ShouldNotBeNull();
                item.StatisticsSummary.ShouldNotBeNull();
                item.Links.ShouldNotBeNull();
            }
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldGetAgenticsWithFilters()
        {
            // Arrange
            var createRequest = CreateValidAgenticRequest("Filtered Test Commerce");
            await DefaultApi.AgenticClient().CreateAgentic(createRequest);

            var listRequest = new GetAgenticsRequest
            {
                Skip = 0,
                Limit = 5,
                IsActive = true,
                NameFilter = "Filtered",
                AiModelFilter = "gpt-4",
                SortBy = "name",
                SortDirection = "asc"
            };

            // Act
            var listResponse = await DefaultApi.AgenticClient().GetAgentics(listRequest);

            // Assert
            listResponse.ShouldNotBeNull();
            listResponse.Items.ShouldNotBeNull();
            
            // All returned items should match the filter criteria
            foreach (var item in listResponse.Items)
            {
                item.Name.ShouldContain("Filtered");
                item.AiModel.ShouldBe("gpt-4");
                item.IsActive.ShouldBe(true);
            }
        }

        [Fact(Skip = "This test requires live API endpoint implementation")]
        private async Task ShouldDeleteAgentic()
        {
            // Arrange
            var createRequest = CreateValidAgenticRequest();
            var createResponse = await DefaultApi.AgenticClient().CreateAgentic(createRequest);

            // Act
            var deleteResponse = await DefaultApi.AgenticClient().DeleteAgentic(createResponse.Id);

            // Assert
            deleteResponse.ShouldNotBeNull();
            deleteResponse.Id.ShouldBe(createResponse.Id);
            deleteResponse.Status.ShouldNotBeNull();
            deleteResponse.DeletedAt.ShouldNotBeNull();
            deleteResponse.Message.ShouldNotBeNull();

            // Verify the record is deleted by trying to get it
            Should.Throw<CheckoutApiException>(async () => 
                await DefaultApi.AgenticClient().GetAgentic(createResponse.Id));
        }

        private CreateAgenticRequest CreateValidAgenticRequest(string name = null)
        {
            return new CreateAgenticRequest
            {
                Name = name ?? "Test Agentic",
                Description = "Test agentic for integration testing",
                Configuration = new AgenticConfiguration
                {
                    AiModel = "gpt-4",
                    MaxAutonomousActions = 100,
                    EnableAutonomousPayments = true,
                    RiskThreshold = 0.8m,
                    AllowedPaymentMethods = new List<string> { "card", "bank_transfer", "digital_wallet" }
                },
                Metadata = new Dictionary<string, object>
                {
                    { "test_environment", "sandbox" },
                    { "created_by", "integration_test" },
                    { "version", "1.0" }
                },
                WebhookEndpoints = new List<string>
                {
                    "https://api.example.com/webhooks/agentic/events",
                    "https://backup.example.com/webhooks/agentic"
                }
            };
        }
    }
}