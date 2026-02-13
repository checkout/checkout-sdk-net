using Checkout.Identities.AmlScreening.Requests;
using Checkout.Identities.AmlScreening.Responses;
using Checkout.Identities.Entities;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.AmlScreening
{
    public class AmlScreeningIntegrationTest : SandboxTestFixture
    {
        public AmlScreeningIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid applicant ID and AML configuration")]
        private async Task ShouldCreateAmlScreening()
        {
            // Arrange
            var amlScreeningRequest = CreateAmlScreeningRequest();

            // Act
            var amlScreeningResponse = await DefaultApi.AmlScreeningClient()
                .CreateAmlScreening(amlScreeningRequest);

            // Assert
            ValidateCreatedAmlScreening(amlScreeningResponse, amlScreeningRequest);
        }

        [Fact(Skip = "This test requires valid applicant ID and AML configuration")]
        private async Task ShouldGetAmlScreening()
        {
            // Arrange
            var amlScreeningRequest = CreateAmlScreeningRequest();
            var createdAmlScreening = await DefaultApi.AmlScreeningClient()
                .CreateAmlScreening(amlScreeningRequest);

            // Act
            var retrievedAmlScreening = await DefaultApi.AmlScreeningClient()
                .GetAmlScreening(createdAmlScreening.Id);

            // Assert
            ValidateRetrievedAmlScreening(retrievedAmlScreening, createdAmlScreening);
        }

        [Fact(Skip = "This test requires valid applicant ID and AML configuration")]
        private async Task ShouldCreateAndTrackAmlScreeningWorkflow()
        {
            // Arrange
            var amlScreeningRequest = CreateAmlScreeningRequest();

            // Act - Create AML screening
            var createdAmlScreening = await DefaultApi.AmlScreeningClient()
                .CreateAmlScreening(amlScreeningRequest);

            // Wait for processing (simulating real-world usage)
            await Task.Delay(TimeSpan.FromSeconds(2));

            // Act - Get updated status
            var updatedAmlScreening = await DefaultApi.AmlScreeningClient()
                .GetAmlScreening(createdAmlScreening.Id);

            // Assert
            ValidateWorkflowProgression(createdAmlScreening, updatedAmlScreening);
        }

        [Fact(Skip = "This test requires valid applicant ID and AML configuration")]
        private async Task ShouldValidateMonitoringConfiguration()
        {
            // Arrange
            var amlScreeningRequest = CreateAmlScreeningRequest();
            amlScreeningRequest.Monitored = false; // Test with monitoring disabled

            // Act
            var amlScreeningResponse = await DefaultApi.AmlScreeningClient()
                .CreateAmlScreening(amlScreeningRequest);

            // Assert
            ValidateCreatedAmlScreening(amlScreeningResponse, amlScreeningRequest);
            amlScreeningResponse.Monitored.ShouldBe(false);
        }

        private static AmlScreeningRequest CreateAmlScreeningRequest()
        {
            // Note: These would need to be valid test IDs in a real test environment
            return new AmlScreeningRequest
            {
                ApplicantId = GetEnvironmentVariable("CHECKOUT_TEST_APPLICANT_ID", "aplt_test_applicant_id"),
                SearchParameters = new SearchParameters
                {
                    ConfigurationIdentifier = GetEnvironmentVariable("CHECKOUT_TEST_AML_CONFIG_ID", "config_test_id") 
                },
                Monitored = true
            };
        }

        private static string GetEnvironmentVariable(string key, string defaultValue)
        {
            return System.Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }

        private static void ValidateCreatedAmlScreening(AmlScreeningResponse response, AmlScreeningRequest request)
        {
            // Base validation
            ValidateBaseAmlScreeningResponse(response);

            // Request-specific validation
            response.ApplicantId.ShouldBe(request.ApplicantId);
            response.SearchParameters.ConfigurationIdentifier.ShouldBe(request.SearchParameters.ConfigurationIdentifier);
            response.Monitored.ShouldBe(request.Monitored);

            // Status should be initial state
            response.Status.ShouldBeOneOf(AmlScreeningStatus.Created, AmlScreeningStatus.ScreeningInProgress);
        }

        private static void ValidateRetrievedAmlScreening(AmlScreeningResponse retrieved, AmlScreeningResponse original)
        {
            // Base validation
            ValidateBaseAmlScreeningResponse(retrieved);

            // Identity validation
            retrieved.Id.ShouldBe(original.Id);
            retrieved.ApplicantId.ShouldBe(original.ApplicantId);
            retrieved.SearchParameters.ConfigurationIdentifier.ShouldBe(original.SearchParameters.ConfigurationIdentifier);
            retrieved.Monitored.ShouldBe(original.Monitored);

            // Timestamps should be consistent or updated
            retrieved.CreatedOn.ShouldBe(original.CreatedOn);
            if (retrieved.ModifiedOn.HasValue && original.ModifiedOn.HasValue)
            {
                retrieved.ModifiedOn.Value.ShouldBeGreaterThanOrEqualTo(original.ModifiedOn.Value);
            }
        }

        private static void ValidateWorkflowProgression(AmlScreeningResponse initial, AmlScreeningResponse updated)
        {
            // Base validation for both responses
            ValidateBaseAmlScreeningResponse(initial);
            ValidateBaseAmlScreeningResponse(updated);

            // Identity should remain the same
            updated.Id.ShouldBe(initial.Id);
            updated.ApplicantId.ShouldBe(initial.ApplicantId);

            // Status may have progressed
            updated.Status.ShouldBeOneOf(
                AmlScreeningStatus.Created,
                AmlScreeningStatus.ScreeningInProgress, 
                AmlScreeningStatus.Approved,
                AmlScreeningStatus.Declined,
                AmlScreeningStatus.ReviewRequired
            );

            // Timestamps should show progression
            updated.CreatedOn.ShouldBe(initial.CreatedOn);
            if (updated.ModifiedOn.HasValue && initial.ModifiedOn.HasValue)
            {
                updated.ModifiedOn.Value.ShouldBeGreaterThanOrEqualTo(initial.ModifiedOn.Value);
            }
        }

        private static void ValidateBaseAmlScreeningResponse(AmlScreeningResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Id.ShouldStartWith("scr_");
            response.ApplicantId.ShouldNotBeNullOrEmpty(); 
            response.ApplicantId.ShouldStartWith("aplt_");
            response.Status.ShouldNotBeNull();
            response.SearchParameters.ShouldNotBeNull();
            response.SearchParameters.ConfigurationIdentifier.ShouldNotBeNullOrEmpty();
            response.CreatedOn.ShouldNotBeNull();
            response.ModifiedOn.ShouldNotBeNull();
            if (response.CreatedOn.HasValue)
            {
                response.CreatedOn.Value.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            }
            if (response.ModifiedOn.HasValue)
            {
                response.ModifiedOn.Value.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            }
            if (response.ModifiedOn.HasValue && response.CreatedOn.HasValue)
            {
                response.ModifiedOn.Value.ShouldBeGreaterThanOrEqualTo(response.CreatedOn.Value);
            }
        }
    }
}