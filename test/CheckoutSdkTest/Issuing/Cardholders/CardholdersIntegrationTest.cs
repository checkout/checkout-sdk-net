using Checkout.Issuing.Cards;
using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cardholders
{
    public class CardholdersIntegrationTest : SandboxTestFixture
    {
        private readonly CheckoutApi _api;

        public CardholdersIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
            _api = GetIssuingCheckoutApi();
        }

        [Fact]
        private async Task ShouldCreateCardholder()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse response = await _api.IssuingClient().CreateCardholder(request);

            response.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(201);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Type.ShouldBe(CardholderType.Individual);
            response.Status.ShouldBe(CardholderStatus.Active);
            response.Reference.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorInvalidData()
        {
            CardholderRequest request = CardholderBadRequest();

            await AssertInvalidDataSent(_api.IssuingClient().CreateCardholder(request));
        }

        [Fact]
        private async Task ShouldGetCardholder()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse cardholder = await _api.IssuingClient().CreateCardholder(request);

            CardholderDetailsResponse response = await _api.IssuingClient().GetCardholderDetails(cardholder.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Id.ShouldBe(cardholder.Id);
            response.Type.ShouldBe(cardholder.Type);
            response.FirstName.ShouldNotBeNull();
            response.MiddleName.ShouldNotBeNull();
            response.LastName.ShouldNotBeNull();
            response.Email.ShouldNotBeNull();
            response.PhoneNumber.ShouldNotBeNull();
            response.DateOfBirth.ShouldNotBeNull();
            response.BillingAddress.ShouldNotBeNull();
            response.ResidencyAddress.ShouldNotBeNull();
            response.Reference.ShouldNotBeNull();
            response.AccountEntityId.ShouldNotBeNull();
            response.EntityId.ShouldNotBeNull();
            response.CreatedDate.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCardholder()
        {
            await AssertNotFound(_api.IssuingClient().GetCardholderDetails("not_found"));
        }

        [Fact]
        private async Task ShouldGetCardholdersCards()
        {
            CardholderRequest request = CardholderRequest();

            CardholderResponse cardholder = await _api.IssuingClient().CreateCardholder(request);

            CardholderCardsResponse response = await _api.IssuingClient().GetCardholdersCards(cardholder.Id);

            response.HttpStatusCode.ShouldBe(200);
            response.Body.ShouldNotBeNull();
            response.ResponseHeaders.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Cards.ShouldNotBeNull();
            foreach (CardResponse card in response.Cards)
            {
                card.CardholderId.ShouldBe(cardholder.Id);
                card.ClientId.ShouldBe("cli_p6jeowdtuxku3azxgt2qa7kq7a");
            }
        }

        [Fact]
        private async Task ShouldThrowErrorNotFoundCardholderCards()
        {
            await AssertNotFound(_api.IssuingClient().GetCardholdersCards("not_found"));
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

        private static CardholderRequest CardholderBadRequest()
        {
            CardholderRequest request = new CardholderRequest
            {
                Type = CardholderType.Individual,
                Reference = "X-123456-N11",
                EntityId = "ent_fa6psq242dcd6fdn5gifcq1491",
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