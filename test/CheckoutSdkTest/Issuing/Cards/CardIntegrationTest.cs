using Checkout.Common;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Renew;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
using Checkout.Issuing.Common;
using Checkout.Issuing.Common.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cards
{
    public class CardIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private AbstractCardCreateResponse _abstractCardRequest;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            AbstractCardCreateRequest abstractCardCreateRequest = await CreateVirtualCard(cardholderResponse.Id);
            _abstractCardRequest = await Api.IssuingClient().CreateCard(abstractCardCreateRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private void ShouldCreateCard()
        {
            AbstractCardCreateResponse response = _abstractCardRequest;

            response.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.DisplayName.ShouldBe("JOHN KENNEDY");
            response.LastFour.ShouldNotBeNull();
            response.ExpiryMonth.ShouldNotBeNull();
            response.ExpiryYear.ShouldNotBeNull();
            response.BillingCurrency.ShouldNotBeNull();
            response.IssuingCountry.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldThrowErrorInvalidData()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            AbstractCardCreateRequest createRequest = await CardBadRequest(cardholderResponse.Id);

            await AssertInvalidDataSent(Api.IssuingClient().CreateCard(createRequest));
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCard()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            AbstractCardResponse response = await Api.IssuingClient().GetCardDetails(abstractCard.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldBe(abstractCard.Id);
            response.CardholderId.ShouldNotBeNull();
            response.CardProductId.ShouldBe(ProductIdOk);
            response.ClientId.ShouldNotBeNull();
            response.LastFour.ShouldNotBeNull();
            response.ExpiryMonth.ShouldNotBeNull();
            response.ExpiryYear.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
            response.DisplayName.ShouldNotBeNull();
            response.BillingCurrency.ShouldNotBeNull();
            response.IssuingCountry.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.Metadata.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldThrowErrorNotFoundCard()
        {
            await AssertNotFound(Api.IssuingClient().GetCardDetails("not_found"));
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCardDetails()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            AbstractCardResponse response = await Api.IssuingClient().GetCardDetails(abstractCard.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.CardholderId.ShouldNotBeNull();
            response.CardProductId.ShouldNotBeNull();
            response.ClientId.ShouldNotBeNull();
            response.LastFour.ShouldNotBeNull();
            response.ExpiryMonth.ShouldNotBeNull();
            response.ExpiryYear.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
            response.DisplayName.ShouldNotBeNull();
            response.BillingCurrency.ShouldNotBeNull();
            response.IssuingCountry.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.Metadata.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldThrowErrorNotFoundCardCards()
        {
            await AssertNotFound(Api.IssuingClient().GetCardDetails("not_found"));
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldErrollCardThreeDSPassword()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            PasswordThreeDsEnrollmentRequest cardEnrollThreeDsRequest = new PasswordThreeDsEnrollmentRequest()
            {
                Locale = "en-US",
                PhoneNumber = GetPhone(),
                Password = "Xtui43FvfiZ"
            };

            ThreeDsEnrollmentResponse response =
                await Api.IssuingClient().EnrollCardThreeDS(abstractCard.Id, cardEnrollThreeDsRequest);

            response.HttpStatusCode.ShouldBe(202);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldErrollCardThreeDSSecurityQuestion()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            SecurityQuestionThreeDsEnrollmentRequest cardEnrollThreeDsRequest =
                new SecurityQuestionThreeDsEnrollmentRequest
                {
                    Locale = "en-US",
                    PhoneNumber = GetPhone(),
                    SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." }
                };

            ThreeDsEnrollmentResponse response =
                await Api.IssuingClient().EnrollCardThreeDS(abstractCard.Id, cardEnrollThreeDsRequest);

            response.HttpStatusCode.ShouldBe(202);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldUpdatErrollCardThreeDS()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            SecurityQuestionThreeDsUpdateRequest cardEnrollSecurityQuestionThreeDsDetailsRequest = new SecurityQuestionThreeDsUpdateRequest
            {
                Locale = "en-US",
                PhoneNumber = GetPhone(),
                SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." },
            };

            ThreeDsEnrollmentUpdateResponse getResponse =
                await Api.IssuingClient().UpdateCardThreeDSDetails(abstractCard.Id, cardEnrollSecurityQuestionThreeDsDetailsRequest);

            getResponse.HttpStatusCode.ShouldBe(202);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldGetErrollCardThreeDS()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            ThreeDsEnrollmentDetailsResponse response = await Api.IssuingClient().GetCardThreeDSDetails(abstractCard.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Locale.ShouldNotBeNull();
            response.PhoneNumber.CountryCode.ShouldBe("1");
            response.PhoneNumber.Number.ShouldBe("4155552671");
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldActivateCard()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            Resource getResponse = await Api.IssuingClient().ActivateCard(abstractCard.Id);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            AbstractCardResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(abstractCard.Id);

            carDetailsResponse.Id.ShouldBe(abstractCard.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Active);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCardCredentials()
        {
            CardCredentialsQuery query = new CardCredentialsQuery { Credentials = "number,cvc2" };

            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            CardCredentialsResponse getResponse = await Api.IssuingClient().GetCardCredentials(abstractCard.Id, query);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Number.ShouldNotBeNull();
            getResponse.Cvc2.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldUpdateCardDetails()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            var updateRequest = new CardsUpdateRequest
            {
                Reference = "UPDATED-REF-123",
                Metadata = new CardMetadata
                {
                    Udf1 = "UDF1",
                    Udf2 = "UDF2",
                    Udf3 = "UDF3",
                    Udf4 = "UDF4",
                    Udf5 = "UDF5"
                },
                ExpiryMonth = 12,
                ExpiryYear = 2030
            };

            var updateResponse = await Api.IssuingClient().UpdateCardDetails(abstractCard.Id, updateRequest);

            updateResponse.HttpStatusCode.ShouldBe(200);
            updateResponse.Body.ShouldNotBeNull();
            updateResponse.ResponseHeaders.ShouldNotBeNull();
            updateResponse.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldRenewCard()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            var renewRequest = new VirtualCardRenewRequest
            {
                Reference = "RENEW-REF-123"
            };

            var renewResponse = await Api.IssuingClient().RenewCard(abstractCard.Id, renewRequest);

            renewResponse.HttpStatusCode.ShouldBe(201);
            renewResponse.Body.ShouldNotBeNull();
            renewResponse.ResponseHeaders.ShouldNotBeNull();
            renewResponse.Links.ShouldNotBeNull();
            renewResponse.Id.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldRevokeCard()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            AbstractCardCreateRequest createRequest = await CreateVirtualCard(cardholderResponse.Id);

            AbstractCardCreateResponse abstractCard = await Api.IssuingClient().CreateCard(createRequest);

            RevokeCardRequest cardReasonRequest = new RevokeCardRequest { Reason = RevokeReason.ReportedLost };

            Resource getResponse = await Api.IssuingClient().RevokeCard(abstractCard.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            AbstractCardResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(abstractCard.Id);

            carDetailsResponse.Id.ShouldBe(abstractCard.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Revoked);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldScheduleCardRevocation()
        {
            var scheduleRequest = new ScheduleCardRevocationRequest
            {
                RevocationDate = System.DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd")
            };

            var resourceResponse = await Api.IssuingClient().ScheduleCardRevocation(scheduleRequest);

            resourceResponse.HttpStatusCode.ShouldBe(200);
            resourceResponse.Body.ShouldNotBeNull();
            resourceResponse.ResponseHeaders.ShouldNotBeNull();
            resourceResponse.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldDeleteScheduledRevocation()
        {
            AbstractCardCreateResponse abstractCard = _abstractCardRequest;

            var resourceResponse = await Api.IssuingClient().DeleteScheduledRevocation(abstractCard.Id);

            resourceResponse.HttpStatusCode.ShouldBe(200);
            resourceResponse.Body.ShouldNotBeNull();
            resourceResponse.ResponseHeaders.ShouldNotBeNull();
            resourceResponse.Links.ShouldNotBeNull();
        }
        
        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSuspendCard()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            AbstractCardCreateRequest createRequest = await CreateVirtualCard(cardholderResponse.Id);

            AbstractCardCreateResponse abstractCard = await Api.IssuingClient().CreateCard(createRequest);

            await Api.IssuingClient().ActivateCard(abstractCard.Id);

            SuspendCardRequest cardReasonRequest = new SuspendCardRequest() { Reason = SuspendReason.SuspectedLost };

            Resource getResponse = await Api.IssuingClient().SuspendCard(abstractCard.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            AbstractCardResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(abstractCard.Id);

            carDetailsResponse.Id.ShouldBe(abstractCard.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Suspended);
        }

    }
}