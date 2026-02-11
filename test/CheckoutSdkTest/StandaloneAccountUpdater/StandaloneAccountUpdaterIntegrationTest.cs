using System.Threading.Tasks;
using Shouldly;
using System;
using Xunit;

using Checkout.StandaloneAccountUpdater.Entities;
using Checkout.StandaloneAccountUpdater.Requests;
using Checkout.StandaloneAccountUpdater.Responses;

namespace Checkout.StandaloneAccountUpdater
{
    public class StandaloneAccountUpdaterIntegrationTest : SandboxTestFixture
    {
        public StandaloneAccountUpdaterIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires valid account updater credentials and instrument data")]
        public async Task GetUpdatedCardCredentials_WithValidCardRequest_ShouldReturnUpdatedCredentials()
        {
            // Arrange
            var request = CreateValidCardRequest();

            // Act
            var response = await DefaultApi.StandaloneAccountUpdaterClient().GetUpdatedCardCredentials(request);

            // Assert
            ValidateGetUpdatedCardCredentialsResponse(response);
        }

        [Fact(Skip = "This test requires valid account updater credentials and instrument data")]
        public async Task GetUpdatedCardCredentials_WithValidInstrumentRequest_ShouldReturnUpdatedCredentials()
        {
            // Arrange
            var request = CreateValidInstrumentRequest();

            // Act
            var response = await DefaultApi.StandaloneAccountUpdaterClient().GetUpdatedCardCredentials(request);

            // Assert
            ValidateGetUpdatedCardCredentialsResponse(response);
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_WithInvalidRequest_ShouldThrowException()
        {
            // Arrange
            var invalidRequest = CreateInvalidRequest();

            // Act & Assert
            await Should.ThrowAsync<CheckoutApiException>(async () => 
                await DefaultApi.StandaloneAccountUpdaterClient().GetUpdatedCardCredentials(invalidRequest));
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_WithStandardTestCard_ShouldThrow422()
        {
            // Arrange
            var validRequest = CreateExpiredCardRequest();

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutApiException>(async () => 
                await DefaultApi.StandaloneAccountUpdaterClient().GetUpdatedCardCredentials(validRequest));

            // Account updater returns 422 for standard test cards - likely requires cards that exist in the system
            // or specific card numbers that simulate real account updater scenarios
            exception.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
        }

        // Common methods
        private static GetUpdatedCardCredentialsRequest CreateValidCardRequest()
        {
            return CreateCardRequest(DateTime.UtcNow.Year + 1);
        }

        private static GetUpdatedCardCredentialsRequest CreateExpiredCardRequest()
        {
            return CreateCardRequest(DateTime.UtcNow.Year - 1);
        }

        private static GetUpdatedCardCredentialsRequest CreateCardRequest(int expiryYear)
        {
            return new GetUpdatedCardCredentialsRequest
            {
                SourceOptions = new SourceOptions
                {
                    Card = new CardDetails
                    {
                        // Using standard Visa test card number
                        Number = "4242424242424242",
                        ExpiryMonth = 12,
                        // Using an expired date since account updater is meant to update expired/expiring cards
                        ExpiryYear = expiryYear
                    }
                }
            };
        }

        private static GetUpdatedCardCredentialsRequest CreateValidInstrumentRequest()
        {
            return new GetUpdatedCardCredentialsRequest
            {
                SourceOptions = new SourceOptions
                {
                    Instrument = new InstrumentReference
                    {
                        // Using a realistic test instrument ID following the pattern ins_xxxxxxxxxxxxxxxxxxxx (26 chars)
                        // Based on existing source ID patterns found in other tests like src_v5rgkf3gdtpuzjqesyxmyodnya
                        Id = "ins_v5rgkf3gdtpuzjqesyxmyodnya"
                    }
                }
            };
        }

        private static GetUpdatedCardCredentialsRequest CreateInvalidRequest()
        {
            return new GetUpdatedCardCredentialsRequest
            {
                SourceOptions = new SourceOptions
                {
                    Card = new CardDetails
                    {
                        Number = "invalid_card_number",
                        ExpiryMonth = 13, // Invalid month
                        ExpiryYear = 2020 // Expired year
                    }
                }
            };
        }

        private static void ValidateGetUpdatedCardCredentialsResponse(GetUpdatedCardCredentialsResponse response)
        {
            response.ShouldNotBeNull();
            response.AccountUpdateStatus.ShouldNotBeNull();
            
            if (response.AccountUpdateStatus == AccountUpdateStatus.CardUpdated || 
                response.AccountUpdateStatus == AccountUpdateStatus.CardExpiryUpdated)
            {
                response.Card.ShouldNotBeNull();
                response.Card.ExpiryMonth.ShouldNotBeNull();
                response.Card.ExpiryYear.ShouldNotBeNull();
            }
            
            if (response.AccountUpdateStatus == AccountUpdateStatus.UpdateFailed)
            {
                response.AccountUpdateFailureCode.ShouldNotBeNull();
            }
        }
    }
}