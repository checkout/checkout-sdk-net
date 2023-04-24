using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Responses;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cards
{
    public class CardIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private CardResponse _cardRequest;

        public async Task InitializeAsync()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest cardRequest = await CreateVirtualCard(cardholderResponse.Id);
            _cardRequest = await Api.IssuingClient().CreateCard(cardRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private void ShouldCreateCard()
        {
            CardResponse response = _cardRequest;

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
            CardRequest request = await CardBadRequest(cardholderResponse.Id);

            await AssertInvalidDataSent(Api.IssuingClient().CreateCard(request));
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCard()
        {
            CardResponse card = _cardRequest;

            CardDetailsResponse response = await Api.IssuingClient().GetCardDetails(card.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldBe(card.Id);
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
            CardResponse card = _cardRequest;

            CardDetailsResponse response = await Api.IssuingClient().GetCardDetails(card.Id);

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
            CardResponse card = _cardRequest;

            PasswordThreeDSEnrollmentRequest cardEnrollThreeDSRequest = new PasswordThreeDSEnrollmentRequest()
            {
                Locale = "en-US", PhoneNumber = GetPhone(), Password = "Xtui43FvfiZ"
            };

            ThreeDSEnrollmentResponse response =
                await Api.IssuingClient().EnrollCardThreeDS(card.Id, cardEnrollThreeDSRequest);

            response.HttpStatusCode.ShouldBe(202);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldErrollCardThreeDSSecurityQuestion()
        {
            CardResponse card = _cardRequest;

            SecurityQuestionThreeDSEnrollmentRequest cardEnrollThreeDSRequest =
                new SecurityQuestionThreeDSEnrollmentRequest
                {
                    Locale = "en-US",
                    PhoneNumber = GetPhone(),
                    SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." }
                };

            ThreeDSEnrollmentResponse response =
                await Api.IssuingClient().EnrollCardThreeDS(card.Id, cardEnrollThreeDSRequest);

            response.HttpStatusCode.ShouldBe(202);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldUpdatErrollCardThreeDS()
        {
            CardResponse card = _cardRequest;

            ThreeDSUpdateRequest cardEnrollThreeDSDetailsRequest = new ThreeDSUpdateRequest
            {
                Locale = "en-US",
                PhoneNumber = GetPhone(),
                SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." },
                Password = "Xtui43FvfiZ",
            };

            ThreeDSUpdateResponse getResponse =
                await Api.IssuingClient().UpdateCardThreeDSDetails(card.Id, cardEnrollThreeDSDetailsRequest);

            getResponse.HttpStatusCode.ShouldBe(202);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();
            getResponse.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldGetErrollCardThreeDS()
        {
            CardResponse card = _cardRequest;

            ThreeDSEnrollmentDetailsResponse response = await Api.IssuingClient().GetCardThreeDSDetails(card.Id);

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
            CardResponse card = _cardRequest;

            Resource getResponse = await Api.IssuingClient().ActivateCard(card.Id);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Active);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldGetCardCredentials()
        {
            CardCredentialsQuery query = new CardCredentialsQuery { Credentials = "number,cvc2" };

            CardResponse card = _cardRequest;

            CardCredentialsResponse getResponse = await Api.IssuingClient().GetCardCredentials(card.Id, query);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Number.ShouldNotBeNull();
            getResponse.Cvc2.ShouldNotBeNull();
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldSuspendCard()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest request = await CreateVirtualCard(cardholderResponse.Id);

            CardResponse card = await Api.IssuingClient().CreateCard(request);

            await Api.IssuingClient().ActivateCard(card.Id);

            SuspendCardRequest cardReasonRequest = new SuspendCardRequest() { Reason = SuspendReason.SuspectedLost };

            Resource getResponse = await Api.IssuingClient().SuspendCard(card.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Suspended);
        }

        [Fact(Skip = "Avoid creating cards all the time")]
        private async Task ShouldRevokeCard()
        {
            CardholderResponse cardholderResponse = await CreateCardholder();
            CardRequest request = await CreateVirtualCard(cardholderResponse.Id);

            CardResponse card = await Api.IssuingClient().CreateCard(request);

            RevokeCardRequest cardReasonRequest = new RevokeCardRequest { Reason = RevokeReason.ReportedLost };

            Resource getResponse = await Api.IssuingClient().RevokeCard(card.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await Api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Revoked);
        }
    }
}