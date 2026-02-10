using Checkout.Identities.Entities;
using Checkout.Identities.IdentityVerification.Requests;
using Checkout.Identities.IdentityVerification.Responses;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.IdentityVerification
{
    public class IdentityVerificationIntegrationTest : SandboxTestFixture
    {
        public IdentityVerificationIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateIdentityVerificationAndAttempt()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationAndAttemptRequest();

            // Act
            var response = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAndAttempt(createRequest);

            // Assert
            ValidateCreatedIdentityVerificationAndAttempt(response, createRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateIdentityVerification()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();

            // Act
            var response = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            // Assert
            ValidateCreatedIdentityVerification(response, createRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdentityVerification()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            // Act
            var retrieved = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerification(created.Id);

            // Assert
            ValidateRetrievedIdentityVerification(retrieved, created);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldAnonymizeIdentityVerification()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            // Act
            var anonymized = await DefaultApi.IdentityVerificationClient()
                .AnonymizeIdentityVerification(created.Id);

            // Assert
            ValidateAnonymizedIdentityVerification(anonymized);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateIdentityVerificationAttempt()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            var attemptRequest = CreateIdentityVerificationAttemptRequest();

            // Act
            var attemptResponse = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAttempt(created.Id, attemptRequest);

            // Assert
            ValidateCreatedIdentityVerificationAttempt(attemptResponse, attemptRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdentityVerificationAttempts()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            var attemptRequest = CreateIdentityVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAttempt(created.Id, attemptRequest);

            // Act
            var attemptsResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempts(created.Id);

            // Assert
            ValidateRetrievedIdentityVerificationAttempts(attemptsResponse, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdentityVerificationAttempt()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            var attemptRequest = CreateIdentityVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAttempt(created.Id, attemptRequest);

            // Act
            var retrievedAttempt = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempt(created.Id, createdAttempt.Id);

            // Assert
            ValidateRetrievedIdentityVerificationAttempt(retrievedAttempt, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetIdentityVerificationReport()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);

            // Act
            var reportResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationReport(created.Id);

            // Assert
            ValidateIdentityVerificationReport(reportResponse);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldPerformCompleteIdentityVerificationWorkflow()
        {
            // Arrange
            var createAndAttemptRequest = CreateIdentityVerificationAndAttemptRequest();

            // Act & Assert - Create Identity Verification and Attempt
            var createdWithAttempt = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAndAttempt(createAndAttemptRequest);
            ValidateCreatedIdentityVerificationAndAttempt(createdWithAttempt, createAndAttemptRequest);

            // Act & Assert - Get Identity Verification
            var retrieved = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerification(createdWithAttempt.Id);
            ValidateRetrievedIdentityVerificationFromCreatedAndAttempt(retrieved, createdWithAttempt);

            // Act & Assert - Create Additional Attempt
            var attemptRequest = CreateIdentityVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAttempt(createdWithAttempt.Id, attemptRequest);
            ValidateCreatedIdentityVerificationAttempt(createdAttempt, attemptRequest);

            // Act & Assert - Get Attempts
            var attemptsResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempts(createdWithAttempt.Id);
            ValidateRetrievedIdentityVerificationAttempts(attemptsResponse, createdAttempt);

            // Act & Assert - Get Single Attempt
            var retrievedAttempt = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempt(createdWithAttempt.Id, createdAttempt.Id);
            ValidateRetrievedIdentityVerificationAttempt(retrievedAttempt, createdAttempt);

            // Act & Assert - Get Report
            var reportResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationReport(createdWithAttempt.Id);
            ValidateIdentityVerificationReport(reportResponse);

            // Act & Assert - Anonymize
            var anonymized = await DefaultApi.IdentityVerificationClient()
                .AnonymizeIdentityVerification(createdWithAttempt.Id);
            ValidateAnonymizedIdentityVerification(anonymized);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldPerformSeparateCreateAndAttemptWorkflow()
        {
            // Arrange
            var createRequest = CreateIdentityVerificationRequest();

            // Act & Assert - Create Identity Verification
            var created = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerification(createRequest);
            ValidateCreatedIdentityVerification(created, createRequest);

            // Act & Assert - Get Identity Verification
            var retrieved = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerification(created.Id);
            ValidateRetrievedIdentityVerification(retrieved, created);

            // Act & Assert - Create Attempt
            var attemptRequest = CreateIdentityVerificationAttemptRequest();
            var createdAttempt = await DefaultApi.IdentityVerificationClient()
                .CreateIdentityVerificationAttempt(created.Id, attemptRequest);
            ValidateCreatedIdentityVerificationAttempt(createdAttempt, attemptRequest);

            // Act & Assert - Get Attempts
            var attemptsResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempts(created.Id);
            ValidateRetrievedIdentityVerificationAttempts(attemptsResponse, createdAttempt);

            // Act & Assert - Get Single Attempt
            var retrievedAttempt = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationAttempt(created.Id, createdAttempt.Id);
            ValidateRetrievedIdentityVerificationAttempt(retrievedAttempt, createdAttempt);

            // Act & Assert - Get Report
            var reportResponse = await DefaultApi.IdentityVerificationClient()
                .GetIdentityVerificationReport(created.Id);
            ValidateIdentityVerificationReport(reportResponse);

            // Act & Assert - Anonymize
            var anonymized = await DefaultApi.IdentityVerificationClient()
                .AnonymizeIdentityVerification(created.Id);
            ValidateAnonymizedIdentityVerification(anonymized);
        }

        private static IdentityVerificationAndAttemptRequest CreateIdentityVerificationAndAttemptRequest()
        {
            return new IdentityVerificationAndAttemptRequest
            {
                ApplicantId = GenerateRandomId(),
                UserJourneyId = GenerateRandomId(),
                RedirectUrl = "https://example.com/redirect",
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdentityVerificationRequest CreateIdentityVerificationRequest()
        {
            return new IdentityVerificationRequest
            {
                ApplicantId = GenerateRandomId(),
                UserJourneyId = GenerateRandomId(),
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdentityVerificationAttemptRequest CreateIdentityVerificationAttemptRequest()
        {
            return new IdentityVerificationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                }
            };
        }

        private static void ValidateCreatedIdentityVerificationAndAttempt(IdentityVerificationAndAttemptResponse response, IdentityVerificationAndAttemptRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldBe(request.UserJourneyId);
            response.ApplicantId.ShouldBe(request.ApplicantId);
            response.Status.ShouldNotBeNull();
            response.RedirectUrl.ShouldBe(request.RedirectUrl);

            if (response.DeclaredData != null && request.DeclaredData != null)
            {
                response.DeclaredData.Name.ShouldBe(request.DeclaredData.Name);
            }

            if (response.CreatedOn.HasValue)
            {
                response.CreatedOn.Value.ShouldBeInRange(DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(1));
            }
        }

        private static void ValidateCreatedIdentityVerification(IdentityVerificationResponse response, IdentityVerificationRequest request)
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

        private static void ValidateRetrievedIdentityVerification(IdentityVerificationResponse retrieved, IdentityVerificationResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.UserJourneyId.ShouldBe(created.UserJourneyId);
            retrieved.ApplicantId.ShouldBe(created.ApplicantId);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateRetrievedIdentityVerificationFromCreatedAndAttempt(IdentityVerificationResponse retrieved, IdentityVerificationAndAttemptResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.UserJourneyId.ShouldBe(created.UserJourneyId);
            retrieved.ApplicantId.ShouldBe(created.ApplicantId);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateAnonymizedIdentityVerification(IdentityVerificationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateCreatedIdentityVerificationAttempt(IdentityVerificationAttemptResponse response, IdentityVerificationAttemptRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
            
            if (response.RedirectUrl != null)
            {
                response.RedirectUrl.ShouldBe(request.RedirectUrl);
            }
        }

        private static void ValidateRetrievedIdentityVerificationAttempts(IdentityVerificationAttemptsResponse response, IdentityVerificationAttemptResponse createdAttempt)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThan(0);
            response.Data.ShouldContain(attempt => attempt.Id == createdAttempt.Id);
        }

        private static void ValidateRetrievedIdentityVerificationAttempt(IdentityVerificationAttemptResponse retrieved, IdentityVerificationAttemptResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateIdentityVerificationReport(IdentityVerificationReportResponse response)
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