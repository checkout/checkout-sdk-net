using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards.Enroll;
using Checkout.Issuing.Cards.Type;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cards
{
    public class CardIntegrationTest : SandboxTestFixture, IAsyncLifetime
    {
        private readonly CheckoutApi _api;
        private CardResponse _cardRequest;

        public async Task InitializeAsync()
        {
            CardTypeVirtualRequest cardRequest = await CardRequest();
            _cardRequest = await _api.IssuingClient().CreateCard(cardRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public CardIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
            _api = GetIssuingCheckoutApi();
        }

        [Fact]
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

        [Fact]
        private async Task ShouldThrowErrorInvalidData()
        {
            CardTypePhysicalRequest request = await CardBadRequest();

            await AssertInvalidDataSent(_api.IssuingClient().CreateCard(request));
        }

        [Fact]
        private async Task ShouldGetCard()
        {
            CardResponse card = _cardRequest;

            CardDetailsResponse response = await _api.IssuingClient().GetCardDetails(card.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldBe(card.Id);
            response.CardholderId.ShouldNotBeNull();
            response.CardProductId.ShouldBe("pro_3fn6pv2ikshurn36dbd3iysyha");
            response.ClientId.ShouldNotBeNull();
            response.LastFour.ShouldNotBeNull();
            response.ExpiryMonth.ShouldNotBeNull();
            response.ExpiryYear.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
            response.DisplayName.ShouldNotBeNull();
            response.Type.ShouldNotBeNull();
            response.BillingCurrency.ShouldNotBeNull();
            response.IssuingCountry.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCard()
        {
            await AssertNotFound(_api.IssuingClient().GetCardDetails("not_found"));
        }

        [Fact]
        private async Task ShouldGetCardDetails()
        {
            CardResponse card = _cardRequest;

            CardDetailsResponse response = await _api.IssuingClient().GetCardDetails(card.Id);

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
            response.Type.ShouldNotBeNull();
            response.BillingCurrency.ShouldNotBeNull();
            response.IssuingCountry.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCardCards()
        {
            await AssertNotFound(_api.IssuingClient().GetCardDetails("not_found"));
        }

        [Fact(Skip = "Client id must be configured for 3ds")]
        private async Task ShouldErrollCardThreeDSPassword()
        {
            CardResponse card = _cardRequest;

            CardEnrollThreeDSPasswordRequest cardEnrollThreeDSRequest = new CardEnrollThreeDSPasswordRequest()
            {
                Locale = "en-US", PhoneNumber = GetPhone(), Password = "Xtui43FvfiZ"
            };

            CardEnrollThreeDSResponse response =
                await _api.IssuingClient().EnrollCardThreeDS(card.Id, cardEnrollThreeDSRequest);

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

            CardEnrollThreeDSSecurityQuestionRequest cardEnrollThreeDSRequest =
                new CardEnrollThreeDSSecurityQuestionRequest
                {
                    Locale = "en-US",
                    PhoneNumber = GetPhone(),
                    SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." }
                };

            CardEnrollThreeDSResponse response =
                await _api.IssuingClient().EnrollCardThreeDS(card.Id, cardEnrollThreeDSRequest);

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

            CardEnrollThreeDSDetailsRequest cardEnrollThreeDSDetailsRequest = new CardEnrollThreeDSDetailsRequest
            {
                Locale = "en-US",
                PhoneNumber = GetPhone(),
                SecurityPair = new SecurityPair { Question = "Who are you?", Answer = "Bond. James Bond." },
                Password = "Xtui43FvfiZ",
            };

            CardEnrollThreeDSDetailsUpdateResponse getResponse =
                await _api.IssuingClient().UpdateCardThreeDSDetails(card.Id, cardEnrollThreeDSDetailsRequest);

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

            CardEnrollThreeDSDetailsGetResponse getResponse = await _api.IssuingClient().GetCardThreeDSDetails(card.Id);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();
            getResponse.Locale.ShouldNotBeNull();
            getResponse.PhoneNumber.CountryCode.ShouldBe("1");
            getResponse.PhoneNumber.Number.ShouldBe("4155552671");
            getResponse.CreatedDate.ShouldNotBeNull();
            getResponse.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldActivateCard()
        {
            CardResponse card = _cardRequest;

            Resource getResponse = await _api.IssuingClient().ActivateCard(card.Id);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await _api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Active);
        }

        [Fact]
        private async Task ShouldGetCardCredentials()
        {
            CardCredentialsQuery query = new CardCredentialsQuery { Credentials = "number,cvc2" };

            CardResponse card = _cardRequest;

            CardCredentialsResponse getResponse = await _api.IssuingClient().GetCardCredentials(card.Id, query);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Number.ShouldNotBeNull();
            getResponse.Cvc2.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldSuspendCard()
        {
            CardTypeVirtualRequest request = await CardRequest();

            CardResponse card = await _api.IssuingClient().CreateCard(request);
            
            Resource activatedCard = await _api.IssuingClient().ActivateCard(card.Id);

            CardReasonRequest cardReasonRequest = new CardReasonRequest { Reason = CardReasonType.SuspectedLost };

            Resource getResponse = await _api.IssuingClient().SuspendCard(card.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await _api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Suspended);
        }
        
        [Fact]
        private async Task ShouldRevokeCard()
        {
            CardTypeVirtualRequest request = await CardRequest();

            CardResponse card = await _api.IssuingClient().CreateCard(request);

            CardReasonRequest cardReasonRequest = new CardReasonRequest { Reason = CardReasonType.ReportedLost };

            Resource getResponse = await _api.IssuingClient().RevokeCard(card.Id, cardReasonRequest);

            getResponse.HttpStatusCode.ShouldBe(200);
            getResponse.Body.ShouldNotBeNull();
            getResponse.ResponseHeaders.ShouldNotBeNull();
            getResponse.Links.ShouldNotBeNull();

            CardDetailsResponse carDetailsResponse = await _api.IssuingClient().GetCardDetails(card.Id);

            carDetailsResponse.Id.ShouldBe(card.Id);
            carDetailsResponse.Status.ShouldBe(CardStatus.Revoked);
        }

        private async Task<CardTypeVirtualRequest> CardRequest()
        {
            CardholderRequest cardholderRequest = CardholderRequest();

            CardholderResponse cardholderResponse = await _api.IssuingClient().CreateCardholder(cardholderRequest);

            CardTypeVirtualRequest cardTypeRequest = new CardTypeVirtualRequest
            {
                Type = CardType.Virtual,
                CardholderId = cardholderResponse.Id,
                Lifetime = new CardLifetime { Unit = UnitType.Months, Value = 6 },
                Reference = "X-123456-N11'",
                CardProductId = "pro_3fn6pv2ikshurn36dbd3iysyha",
                DisplayName = "JOHN KENNEDY",
                IsSingleUse = false,
                ActivateCard = false
            };
            return cardTypeRequest;
        }

        private async Task<CardTypePhysicalRequest> CardBadRequest()
        {
            CardholderRequest cardholderRequest = CardholderRequest();

            CardholderResponse cardholderResponse = await _api.IssuingClient().CreateCardholder(cardholderRequest);

            CardTypePhysicalRequest cardTypeRequest = new CardTypePhysicalRequest()
            {
                Type = CardType.Physical,
                CardholderId = cardholderResponse.Id,
                Lifetime = new CardLifetime { Unit = UnitType.Months, Value = 6 },
                Reference = "X-123456-N11'",
                CardProductId = "pro_2ebzpnw3wvcefnu7fqglqmg56m",
                DisplayName = "JOHN KENNEDY",
                ShippingInstructions = new IssuingShippingInstructions
                {
                    ShippingRecipient = "john kennedy", ShippingAddress = GetAddress(), AdditionalComment = null
                },
                ActivateCard = false
            };
            return cardTypeRequest;
        }

        private static CardholderRequest CardholderRequest()
        {
            CardholderRequest request = new CardholderRequest
            {
                Type = CardholderType.Individual,
                Reference = "X-123456-N11",
                EntityId = "ent_mujh2nia2ypezmw5fo2fofk7ka",
                FirstName = "John",
                MiddleName = "Fitzgerald",
                LastName = "Kennedy",
                Email = "john.kennedy@myemaildomain.com",
                PhoneNumber = GetPhone(),
                DateOfBirth = "1985-05-15",
                BillingAddress = GetAddress(),
                ResidencyAddress = GetAddress(),
                Document = new CardholderDocument
                {
                    Type = DocumentType.Passport,
                    FrontDocumentId = "file_6lbss42ezvoufcb2beo76rvwly",
                    BackDocumentId = "file_aaz5pemp6326zbuvevp6qroqu4"
                }
            };
            return request;
        }

        private static CheckoutApi GetIssuingCheckoutApi()
        {
            return CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET"))
                .Scopes(OAuthScope.IssuingCard, OAuthScope.IssuingClient, OAuthScope.Vault)
                .Build() as CheckoutApi;
        }
    }
}