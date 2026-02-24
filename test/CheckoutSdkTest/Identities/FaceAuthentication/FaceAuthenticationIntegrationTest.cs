using Checkout.Identities.Entities;
using Checkout.Identities.FaceAuthentication.Requests;
using Checkout.Identities.FaceAuthentication.Responses;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.FaceAuthentication
{
    public class FaceAuthenticationIntegrationTest : SandboxTestFixture
    {
        public FaceAuthenticationIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateFaceAuthentication()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();

            // Act
            var faceAuthenticationResponse = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            // Assert
            ValidateCreatedFaceAuthentication(faceAuthenticationResponse, createFaceAuthenticationRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetFaceAuthentication()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            // Act
            var retrievedFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthentication(createdFaceAuthentication.Id);

            // Assert
            ValidateRetrievedFaceAuthentication(retrievedFaceAuthentication, createdFaceAuthentication);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldAnonymizeFaceAuthentication()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            // Act
            var anonymizedFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .AnonymizeFaceAuthentication(createdFaceAuthentication.Id);

            // Assert
            ValidateAnonymizedFaceAuthentication(anonymizedFaceAuthentication);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldCreateFaceAuthenticationAttempt()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            var createAttemptRequest = CreateFaceAuthenticationAttemptRequest();

            // Act
            var attemptResponse = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthenticationAttempt(createdFaceAuthentication.Id, createAttemptRequest);

            // Assert
            ValidateCreatedFaceAuthenticationAttempt(attemptResponse, createAttemptRequest);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetFaceAuthenticationAttempts()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            var createAttemptRequest = CreateFaceAuthenticationAttemptRequest();
            var createdAttempt = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthenticationAttempt(createdFaceAuthentication.Id, createAttemptRequest);

            // Act
            var attemptsResponse = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthenticationAttempts(createdFaceAuthentication.Id);

            // Assert
            ValidateRetrievedFaceAuthenticationAttempts(attemptsResponse, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetFaceAuthenticationAttempt()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);

            var createAttemptRequest = CreateFaceAuthenticationAttemptRequest();
            var createdAttempt = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthenticationAttempt(createdFaceAuthentication.Id, createAttemptRequest);

            // Act
            var retrievedAttempt = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthenticationAttempt(createdFaceAuthentication.Id, createdAttempt.Id);

            // Assert
            ValidateRetrievedFaceAuthenticationAttempt(retrievedAttempt, createdAttempt);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldPerformFaceAuthenticationWorkflow()
        {
            // Arrange
            var createFaceAuthenticationRequest = CreateFaceAuthenticationRequest();

            // Act & Assert - Create Face Authentication
            var createdFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthentication(createFaceAuthenticationRequest);
            ValidateCreatedFaceAuthentication(createdFaceAuthentication, createFaceAuthenticationRequest);

            // Act & Assert - Get Face Authentication
            var retrievedFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthentication(createdFaceAuthentication.Id);
            ValidateRetrievedFaceAuthentication(retrievedFaceAuthentication, createdFaceAuthentication);

            // Act & Assert - Create Attempt
            var createAttemptRequest = CreateFaceAuthenticationAttemptRequest();
            var createdAttempt = await DefaultApi.FaceAuthenticationClient()
                .CreateFaceAuthenticationAttempt(createdFaceAuthentication.Id, createAttemptRequest);
            ValidateCreatedFaceAuthenticationAttempt(createdAttempt, createAttemptRequest);

            // Act & Assert - Get Attempts
            var attemptsResponse = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthenticationAttempts(createdFaceAuthentication.Id);
            ValidateRetrievedFaceAuthenticationAttempts(attemptsResponse, createdAttempt);

            // Act & Assert - Get Single Attempt
            var retrievedAttempt = await DefaultApi.FaceAuthenticationClient()
                .GetFaceAuthenticationAttempt(createdFaceAuthentication.Id, createdAttempt.Id);
            ValidateRetrievedFaceAuthenticationAttempt(retrievedAttempt, createdAttempt);

            // Act & Assert - Anonymize
            var anonymizedFaceAuthentication = await DefaultApi.FaceAuthenticationClient()
                .AnonymizeFaceAuthentication(createdFaceAuthentication.Id);
            ValidateAnonymizedFaceAuthentication(anonymizedFaceAuthentication);
        }

        private static FaceAuthenticationRequest CreateFaceAuthenticationRequest()
        {
            return new FaceAuthenticationRequest
            {
                ApplicantId = GenerateRandomId(),
                UserJourneyId = GenerateRandomId()
            };
        }

        private static FaceAuthenticationAttemptRequest CreateFaceAuthenticationAttemptRequest()
        {
            return new FaceAuthenticationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                }
            };
        }

        private static void ValidateCreatedFaceAuthentication(FaceAuthenticationResponse response, FaceAuthenticationRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldBe(request.UserJourneyId);
            response.ApplicantId.ShouldBe(request.ApplicantId);
            response.Status.ShouldNotBeNull();
            
            if (response.CreatedOn.HasValue)
            {
                response.CreatedOn.Value.ShouldBeInRange(DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(1));
            }
        }

        private static void ValidateRetrievedFaceAuthentication(FaceAuthenticationResponse retrieved, FaceAuthenticationResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.UserJourneyId.ShouldBe(created.UserJourneyId);
            retrieved.ApplicantId.ShouldBe(created.ApplicantId);
            retrieved.Status.ShouldNotBeNull();
        }

        private static void ValidateAnonymizedFaceAuthentication(FaceAuthenticationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateCreatedFaceAuthenticationAttempt(FaceAuthenticationAttemptResponse response, FaceAuthenticationAttemptRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
            
            if (response.RedirectUrl != null)
            {
                response.RedirectUrl.ShouldBe(request.RedirectUrl);
            }
        }

        private static void ValidateRetrievedFaceAuthenticationAttempts(FaceAuthenticationAttemptsResponse response, FaceAuthenticationAttemptResponse createdAttempt)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThan(0);
            response.Data.ShouldContain(attempt => attempt.Id == createdAttempt.Id);
        }

        private static void ValidateRetrievedFaceAuthenticationAttempt(FaceAuthenticationAttemptResponse retrieved, FaceAuthenticationAttemptResponse created)
        {
            retrieved.ShouldNotBeNull();
            retrieved.Id.ShouldBe(created.Id);
            retrieved.Status.ShouldNotBeNull();
        }

        private static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString("N")[..16];
        }
    }
}