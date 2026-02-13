using Checkout.Identities.Entities;
using Checkout.Identities.IdDocumentVerification.Requests;
using Checkout.Identities.IdDocumentVerification.Responses;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.IdDocumentVerification
{
    public class IdDocumentVerificationIntegrationTest : SandboxTestFixture
    {
        public IdDocumentVerificationIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateIdDocumentVerification()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();

            // Act
            var idDocumentVerificationResponse = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            // Assert
            ValidateCreatedIdDocumentVerification(idDocumentVerificationResponse, createIdDocumentVerificationRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdDocumentVerification()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            // Act
            var retrievedIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerification(createdIdDocumentVerification.Id);

            // Assert
            ValidateRetrievedIdDocumentVerification(retrievedIdDocumentVerification, createdIdDocumentVerification);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldAnonymizeIdDocumentVerification()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            // Act
            var anonymizedIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .AnonymizeIdDocumentVerification(createdIdDocumentVerification.Id);

            // Assert
            ValidateAnonymizedIdDocumentVerification(anonymizedIdDocumentVerification);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateIdDocumentVerificationAttempt()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            var createAttemptRequest = CreateIdDocumentVerificationAttemptRequest();

            // Act
            var attemptResponse = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createAttemptRequest);

            // Assert
            ValidateCreatedIdDocumentVerificationAttempt(attemptResponse, createAttemptRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdDocumentVerificationAttempts()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            var createAttemptRequest = CreateIdDocumentVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createAttemptRequest);

            // Act
            var attemptsResponse = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationAttempts(createdIdDocumentVerification.Id);

            // Assert
            ValidateRetrievedIdDocumentVerificationAttempts(attemptsResponse, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdDocumentVerificationAttempt()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            var createAttemptRequest = CreateIdDocumentVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createAttemptRequest);

            // Act
            var retrievedAttempt = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createdAttempt.Id);

            // Assert
            ValidateRetrievedIdDocumentVerificationAttempt(retrievedAttempt, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdDocumentVerificationReport()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);

            // Act
            var reportResponse = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationReport(createdIdDocumentVerification.Id);

            // Assert
            ValidateIdDocumentVerificationReport(reportResponse);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldPerformIdDocumentVerificationWorkflow()
        {
            // Arrange
            var createIdDocumentVerificationRequest = CreateIdDocumentVerificationRequest();

            // Act & Assert - Create ID Document Verification
            var createdIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerification(createIdDocumentVerificationRequest);
            ValidateCreatedIdDocumentVerification(createdIdDocumentVerification, createIdDocumentVerificationRequest);

            // Act & Assert - Get ID Document Verification
            var retrievedIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerification(createdIdDocumentVerification.Id);
            ValidateRetrievedIdDocumentVerification(retrievedIdDocumentVerification, createdIdDocumentVerification);

            // Act & Assert - Create Attempt
            var createAttemptRequest = CreateIdDocumentVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdDocumentVerificationClient()
                .CreateIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createAttemptRequest);
            ValidateCreatedIdDocumentVerificationAttempt(createdAttempt, createAttemptRequest);

            // Act & Assert - Get Attempts
            var attemptsResponse = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationAttempts(createdIdDocumentVerification.Id);
            ValidateRetrievedIdDocumentVerificationAttempts(attemptsResponse, createdAttempt);

            // Act & Assert - Get Single Attempt
            var retrievedAttempt = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationAttempt(createdIdDocumentVerification.Id, createdAttempt.Id);
            ValidateRetrievedIdDocumentVerificationAttempt(retrievedAttempt, createdAttempt);

            // Act & Assert - Get Report
            var reportResponse = await DefaultApi.IdDocumentVerificationClient()
                .GetIdDocumentVerificationReport(createdIdDocumentVerification.Id);
            ValidateIdDocumentVerificationReport(reportResponse);

            // Act & Assert - Anonymize
            var anonymizedIdDocumentVerification = await DefaultApi.IdDocumentVerificationClient()
                .AnonymizeIdDocumentVerification(createdIdDocumentVerification.Id);
            ValidateAnonymizedIdDocumentVerification(anonymizedIdDocumentVerification);
        }

        private static IdDocumentVerificationRequest CreateIdDocumentVerificationRequest()
        {
            return new IdDocumentVerificationRequest
            {
                ApplicantId = GenerateRandomId(),
                UserJourneyId = GenerateRandomId(),
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdDocumentVerificationAttemptRequest CreateIdDocumentVerificationAttemptRequest()
        {
            return new IdDocumentVerificationAttemptRequest
            {
                DocumentFront = "base64-encoded-front-image-data",
                DocumentBack = "base64-encoded-back-image-data"
            };
        }

        private static void ValidateCreatedIdDocumentVerification(IdDocumentVerificationResponse response, IdDocumentVerificationRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldBe(request.UserJourneyId);
            response.ApplicantId.ShouldBe(request.ApplicantId);
            response.Status.ShouldNotBeNull();
            
            if (response.DeclaredData != null && request.DeclaredData != null)
            {
                response.DeclaredData.Name.ShouldBe(request.DeclaredData.Name);
            }

            if (response.CreatedOn.HasValue)
            {
                response.CreatedOn.Value.ShouldBeInRange(DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(1));
            }
        }

        private static void ValidateRetrievedIdDocumentVerification(IdDocumentVerificationResponse retrieved, IdDocumentVerificationResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.UserJourneyId.ShouldBe(created.UserJourneyId);
            retrieved.ApplicantId.ShouldBe(created.ApplicantId);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateAnonymizedIdDocumentVerification(IdDocumentVerificationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateCreatedIdDocumentVerificationAttempt(IdDocumentVerificationAttemptResponse response, IdDocumentVerificationAttemptRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateRetrievedIdDocumentVerificationAttempts(IdDocumentVerificationAttemptsResponse response, IdDocumentVerificationAttemptResponse createdAttempt)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThan(0);
            response.Data.ShouldContain(attempt => attempt.Id == createdAttempt.Id);
        }

        private static void ValidateRetrievedIdDocumentVerificationAttempt(IdDocumentVerificationAttemptResponse retrieved, IdDocumentVerificationAttemptResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateIdDocumentVerificationReport(IdDocumentVerificationReportResponse response)
        {
            response.ShouldNotBeNull();
            response.SignedUrl.ShouldNotBeNullOrEmpty();
            response.SignedUrl.ShouldStartWith("https://");
        }

        private static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString("N")[..16];
        }
    }
}