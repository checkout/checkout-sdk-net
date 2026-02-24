using Checkout.Identities.Applicants.Requests;
using Checkout.Identities.Applicants.Responses;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.Applicants
{
    public class ApplicantsIntegrationTest : SandboxTestFixture
    {
        public ApplicantsIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateApplicant()
        {
            // Arrange
            var createApplicantRequest = CreateApplicantRequest();

            // Act
            var applicantResponse = await DefaultApi.ApplicantsClient()
                .CreateApplicant(createApplicantRequest);

            // Assert
            ValidateCreatedApplicant(applicantResponse, createApplicantRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetApplicant()
        {
            // Arrange
            var createApplicantRequest = CreateApplicantRequest();
            var createdApplicant = await DefaultApi.ApplicantsClient()
                .CreateApplicant(createApplicantRequest);

            // Act
            var retrievedApplicant = await DefaultApi.ApplicantsClient()
                .GetApplicant(createdApplicant.Id);

            // Assert
            ValidateRetrievedApplicant(retrievedApplicant, createdApplicant);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldUpdateApplicant()
        {
            // Arrange
            var createApplicantRequest = CreateApplicantRequest();
            var createdApplicant = await DefaultApi.ApplicantsClient()
                .CreateApplicant(createApplicantRequest);
            
            var updateApplicantRequest = CreateUpdateApplicantRequest();

            // Act
            var updatedApplicant = await DefaultApi.ApplicantsClient()
                .UpdateApplicant(createdApplicant.Id, updateApplicantRequest);

            // Assert
            ValidateUpdatedApplicant(updatedApplicant, createdApplicant, updateApplicantRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldAnonymizeApplicant()
        {
            // Arrange
            var createApplicantRequest = CreateApplicantRequest();
            var createdApplicant = await DefaultApi.ApplicantsClient()
                .CreateApplicant(createApplicantRequest);

            // Act
            var anonymizeResponse = await DefaultApi.ApplicantsClient().AnonymizeApplicant(createdApplicant.Id);

            // Assert
            anonymizeResponse.ShouldNotBeNull();
            anonymizeResponse.Id.ShouldBe(createdApplicant.Id);
            ValidateBaseApplicantResponse(anonymizeResponse);            
            
            // Verify applicant is no longer accessible (should throw or return null/error)
            await Should.ThrowAsync<Exception>(async () =>
                await DefaultApi.ApplicantsClient().GetApplicant(createdApplicant.Id)
            );
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateUpdateAndRetrieveApplicantWorkflow()
        {
            // Arrange
            var createApplicantRequest = CreateApplicantRequest();

            // Act - Create applicant
            var createdApplicant = await DefaultApi.ApplicantsClient()
                .CreateApplicant(createApplicantRequest);

            // Act - Update applicant
            var updateApplicantRequest = CreateUpdateApplicantRequest();
            var updatedApplicant = await DefaultApi.ApplicantsClient()
                .UpdateApplicant(createdApplicant.Id, updateApplicantRequest);

            // Act - Retrieve updated applicant
            var retrievedApplicant = await DefaultApi.ApplicantsClient()
                .GetApplicant(createdApplicant.Id);

            // Assert
            ValidateWorkflowProgression(createdApplicant, updatedApplicant, retrievedApplicant, updateApplicantRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldValidateOptionalFields()
        {
            // Arrange
            var minimalCreateRequest = new CreateApplicantRequest
            {
                Email = GenerateRandomEmail()
                // Leave optional fields null to test minimal requirements
            };

            // Act
            var applicantResponse = await DefaultApi.ApplicantsClient()
                .CreateApplicant(minimalCreateRequest);

            // Assert
            ValidateBaseApplicantResponse(applicantResponse);
            applicantResponse.Email.ShouldBe(minimalCreateRequest.Email);
        }

        private static CreateApplicantRequest CreateApplicantRequest()
        {
            return new CreateApplicantRequest
            {
                ExternalApplicantId = $"ext_test_{Guid.NewGuid():N}",
                Email = GenerateRandomEmail(),
                ExternalApplicantName = "Test Applicant Name"
            };
        }

        private static UpdateApplicantRequest CreateUpdateApplicantRequest()
        {
            return new UpdateApplicantRequest
            {
                Email = $"test.applicant.updated.{Guid.NewGuid():N}@checkout.com",
                ExternalApplicantName = "Updated Test Applicant Name"
            };
        }

        private static void ValidateCreatedApplicant(ApplicantResponse response, CreateApplicantRequest request)
        {
            // Base validation
            ValidateBaseApplicantResponse(response);

            // Request-specific validation
            response.Email.ShouldBe(request.Email);
            response.ExternalApplicantName.ShouldBe(request.ExternalApplicantName);
            response.ExternalApplicantId.ShouldBe(request.ExternalApplicantId);
        }

        private static void ValidateRetrievedApplicant(ApplicantResponse retrieved, ApplicantResponse original)
        {
            // Base validation
            ValidateBaseApplicantResponse(retrieved);

            // Identity validation
            retrieved.Id.ShouldBe(original.Id);
            retrieved.Email.ShouldBe(original.Email);
            retrieved.ExternalApplicantName.ShouldBe(original.ExternalApplicantName);
            retrieved.ExternalApplicantId.ShouldBe(original.ExternalApplicantId);

            // Timestamps should be consistent or updated
            retrieved.CreatedOn.ShouldBe(original.CreatedOn);
            if (retrieved.ModifiedOn.HasValue && original.ModifiedOn.HasValue)
            {
                retrieved.ModifiedOn.Value.ShouldBeGreaterThanOrEqualTo(original.ModifiedOn.Value);
            }
        }

        private static void ValidateUpdatedApplicant(ApplicantResponse updated, ApplicantResponse original, UpdateApplicantRequest updateRequest)
        {
            // Base validation
            ValidateBaseApplicantResponse(updated);

            // Identity should remain the same
            updated.Id.ShouldBe(original.Id);
            updated.ExternalApplicantId.ShouldBe(original.ExternalApplicantId); // This doesn't change in update

            // Updated fields should reflect changes
            updated.Email.ShouldBe(updateRequest.Email);
            updated.ExternalApplicantName.ShouldBe(updateRequest.ExternalApplicantName);

            // Timestamps should show progression
            updated.CreatedOn.ShouldBe(original.CreatedOn);
            if (updated.ModifiedOn.HasValue && original.ModifiedOn.HasValue)
            {
                updated.ModifiedOn.Value.ShouldBeGreaterThanOrEqualTo(original.ModifiedOn.Value);
            }
        }

        private static void ValidateWorkflowProgression(ApplicantResponse created, ApplicantResponse updated, ApplicantResponse retrieved, UpdateApplicantRequest updateRequest)
        {
            // All responses should have same identity
            created.Id.ShouldBe(updated.Id);
            updated.Id.ShouldBe(retrieved.Id);

            // Final state should reflect the update
            retrieved.Email.ShouldBe(updateRequest.Email);
            retrieved.ExternalApplicantName.ShouldBe(updateRequest.ExternalApplicantName);
            retrieved.ExternalApplicantId.ShouldBe(created.ExternalApplicantId); // Should remain unchanged

            // CreatedOn should remain consistent
            created.CreatedOn.ShouldBe(updated.CreatedOn);
            updated.CreatedOn.ShouldBe(retrieved.CreatedOn);
        }

        private static void ValidateBaseApplicantResponse(ApplicantResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Id.ShouldStartWith("aplt_");
            response.Email.ShouldNotBeNullOrEmpty();
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