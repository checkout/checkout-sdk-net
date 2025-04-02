using Checkout.Common;
using Checkout.Issuing.Cardholders;
using Checkout.Issuing.Cards.Requests.Create;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public class IssuingCommon : SandboxTestFixture
    {
        protected readonly CheckoutApi Api;
        protected readonly string ProductIdOk = "pro_3fn6pv2ikshurn36dbd3iysyha";
        protected readonly string ProductIdBad = "pro_2ebzpnw3wvcefnu7fqglqmg56m";

        protected IssuingCommon() : base(PlatformType.DefaultOAuth)
        {
            Api = IssuingCheckoutApi();
        }

        protected Task<CardRequest> CreateVirtualCard(string cardholderId)
        {
            CardRequest cardRequest = new CardVirtualRequest
            {
                CardholderId = cardholderId,
                Lifetime = new CardLifetime { Unit = LifetimeUnit.Months, Value = 6 },
                Reference = "X-123456-N11'",
                CardProductId = ProductIdOk,
                DisplayName = "JOHN KENNEDY",
                ActivateCard = false,
                IsSingleUse = false,
            };
            return Task.FromResult(cardRequest);
        }

        protected Task<CardRequest> CardBadRequest(string cardholderId)
        {
            CardRequest cardRequest = new CardPhysicalRequest()
            {
                CardholderId = cardholderId,
                Lifetime = new CardLifetime { Unit = LifetimeUnit.Months, Value = 6 },
                Reference = "X-123456-N11'",
                CardProductId = ProductIdBad,
                DisplayName = "JOHN KENNEDY",
                ShippingInstructions = new ShippingInstruction
                {
                    ShippingRecipient = "john kennedy", ShippingAddress = GetAddress(), AdditionalComment = null
                },
                ActivateCard = false
            };
            return Task.FromResult(cardRequest);
        }

        protected static CardholderRequest CardholderRequest()
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

        protected async Task<CardholderResponse> CreateCardholder()
        {
            CardholderRequest cardholderRequest = CardholderRequest();

            CardholderResponse cardholderResponse = await Api.IssuingClient().CreateCardholder(cardholderRequest);
            return cardholderResponse;
        }

        protected static CardholderRequest CardholderBadRequest()
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

        protected async Task<CardholderResponse> CreateBadCardholder()
        {
            CardholderRequest cardholderRequest = CardholderBadRequest();

            CardholderResponse cardholderResponse = await Api.IssuingClient().CreateCardholder(cardholderRequest);
            return cardholderResponse;
        }

        private static CheckoutApi IssuingCheckoutApi()
        {
            return CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET"))
                .Scopes(OAuthScope.IssuingCard, OAuthScope.IssuingControlRead, OAuthScope.IssuingControlWrite,
                    OAuthScope.IssuingClient, OAuthScope.IssuingTransactionsRead, OAuthScope.Vault)
                .Environment(Environment.Sandbox)
                .Build() as CheckoutApi;
        }
    }
}