using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.Entities;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.AddressDocumentVerification
{
    /// <summary>
    /// Integration tests for the address document verification attempt-assets endpoint and the
    /// paginated attempts endpoint added by swagger 2026-09-02.
    /// </summary>
    public class AddressDocumentVerificationIntegrationTest : SandboxTestFixture
    {
        public AddressDocumentVerificationIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetAddressDocumentVerificationAttemptAssets()
        {
            // Arrange
            var created = await DefaultApi.AddressDocumentVerificationClient()
                .CreateAddressDocumentVerification(CreateAddressDocumentVerificationRequest());

            var createdAttempt = await DefaultApi.AddressDocumentVerificationClient()
                .CreateAddressDocumentVerificationAttempt(created.Id, CreateAttemptRequest());

            var query = new AttemptAssetsQuery { Skip = 0, Limit = 10 };

            // Act
            var assets = await DefaultApi.AddressDocumentVerificationClient()
                .GetAddressDocumentVerificationAttemptAssets(created.Id, createdAttempt.Id, query);

            // Assert
            assets.ShouldNotBeNull();
            assets.Data.ShouldNotBeNull();
            assets.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
            assets.Limit.ShouldBe(10);
        }

        [Fact(Skip = "This test requires valid test environment setup")]
        private async Task ShouldGetAddressDocumentVerificationAttemptsPaginated()
        {
            // Arrange
            var created = await DefaultApi.AddressDocumentVerificationClient()
                .CreateAddressDocumentVerification(CreateAddressDocumentVerificationRequest());

            await DefaultApi.AddressDocumentVerificationClient()
                .CreateAddressDocumentVerificationAttempt(created.Id, CreateAttemptRequest());

            var query = new AttemptsQuery { Skip = 0, Limit = 5 };

            // Act
            var attempts = await DefaultApi.AddressDocumentVerificationClient()
                .GetAddressDocumentVerificationAttempts(created.Id, query);

            // Assert
            attempts.ShouldNotBeNull();
            attempts.Data.ShouldNotBeNull();
            attempts.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
            attempts.Limit.ShouldBe(5);
        }

        private static AddressDocumentVerificationRequest CreateAddressDocumentVerificationRequest()
        {
            return new AddressDocumentVerificationRequest
            {
                ApplicantId = "aplt_tkoi5db4hryu5cei5vwoabr7we",
                UserJourneyId = "usj_tkoi5db4hryu5cei5vwoabr7we",
                DeclaredData = new DeclaredData
                {
                    Name = "Hannah Bret",
                    BirthDate = "1994-10-15"
                }
            };
        }

        private static AddressDocumentVerificationAttemptRequest CreateAttemptRequest()
        {
            return new AddressDocumentVerificationAttemptRequest { Document = "address_document.png" };
        }
    }
}
