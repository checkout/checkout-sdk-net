using Checkout.Common;
using Checkout.Issuing.Cardholders.Requests;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

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

        protected Task<AbstractCardCreateRequest> CreateVirtualCard(string cardholderId)
        {
            AbstractCardCreateRequest abstractCardCreateRequest = new VirtualCardCreateRequest
            {
                CardholderId = cardholderId,
                Lifetime = new CardLifetime { Unit = LifetimeUnit.Months, Value = 6 },
                Reference = "X-123456-N11'",
                CardProductId = ProductIdOk,
                DisplayName = "JOHN KENNEDY",
                ActivateCard = false,
                IsSingleUse = false,
                Metadata = new CardMetadata
                {
                    Udf1 = "UDF1",
                    Udf2 = "UDF2",
                    Udf3 = "UDF3",
                    Udf4 = "UDF4",
                    Udf5 = "UDF5",
                }
            };
            return Task.FromResult(abstractCardCreateRequest);
        }

        protected Task<AbstractCardCreateRequest> CardBadRequest(string cardholderId)
        {
            AbstractCardCreateRequest abstractCardCreateRequest = new PhysicalCardCreateRequest()
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
            return Task.FromResult(abstractCardCreateRequest);
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
            var logFactory = TestLoggerFactory.Create();
            
            return CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET"))
                .Scopes(OAuthScope.IssuingCard, OAuthScope.IssuingControlRead, OAuthScope.IssuingControlWrite,
                    OAuthScope.IssuingClient, OAuthScope.IssuingTransactionsRead, OAuthScope.Vault)
                .Environment(Environment.Sandbox)
                .LogProvider(logFactory)
                .Build() as CheckoutApi;
        }
    }
}