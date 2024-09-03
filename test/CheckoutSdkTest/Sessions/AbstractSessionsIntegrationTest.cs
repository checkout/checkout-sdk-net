using Checkout.Common;
using Checkout.Sessions.Channel;
using Checkout.Sessions.Completion;
using Checkout.Sessions.Source;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sessions
{
    public abstract class AbstractSessionsIntegrationTest : SandboxTestFixture
    {
        protected AbstractSessionsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        protected async Task<SessionResponse> CreateNonHostedSession(ChannelData channelData,
            Category authenticationCategory,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var billingAddress = new SessionAddress
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "ENG",
                Country = CountryCode.GB
            };

            var source = new SessionCardSource
            {
                Number = TestCardSource.Visa.Number,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                Name = "John Doe",
                Email = GenerateRandomEmail(),
                BillingAddress = billingAddress,
                HomePhone = GetPhone(),
                MobilePhone = GetPhone(),
                WorkPhone = GetPhone()
            };

            var shippingAddress = new SessionAddress
            {
                AddressLine1 = "Checkout.com",
                AddressLine2 = "ABC building",
                AddressLine3 = "14 Wells Mews",
                City = "London",
                State = "ENG",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var sessionRequest = new SessionRequest
            {
                Source = source,
                Amount = 6540L,
                Currency = Currency.USD,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Marketplace = new SessionMarketplaceData {SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u"},
                AuthenticationCategory = authenticationCategory,
                ChallengeIndicator = challengeIndicator,
                BillingDescriptor = new SessionsBillingDescriptor {Name = "SUPERHEROES.COM"},
                Reference = "ORD-5023-4E89",
                TransactionType = transactionType,
                ShippingAddress = shippingAddress,
                Completion = new NonHostedCompletionInfo {CallbackUrl = "https://merchant.com/callback"},
                ChannelData = channelData
            };

            return await DefaultApi.SessionsClient().RequestSession(sessionRequest, CancellationToken.None);
        }

        protected async Task<SessionResponse> CreateHostedSession()
        {
            var shippingAddress = new SessionAddress
            {
                AddressLine1 = "Checkout.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "ENG",
                Country = CountryCode.GB,
                Zip = "W1T 4TJ"
            };

            var sessionRequest = new SessionRequest
            {
                Source = new SessionCardSource {ExpiryMonth = 1, ExpiryYear = 2030, Number = "4485040371536584"},
                Amount = 100L,
                Currency = Currency.USD,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                AuthenticationType = AuthenticationType.Regular,
                AuthenticationCategory = Category.Payment,
                ChallengeIndicator = ChallengeIndicatorType.NoPreference,
                Reference = "ORD-5023-4E89",
                TransactionType = TransactionType.GoodsService,
                ShippingAddress = shippingAddress,
                Completion = new HostedCompletionInfo
                {
                    FailureUrl = "https://example.com/sessions/fail",
                    SuccessUrl = "https://example.com/sessions/success",
                }
            };

            return await Retriable(
                async () => await DefaultApi.SessionsClient().RequestSession(sessionRequest, CancellationToken.None),
                HasSessionAccepted);
        }

        protected static ChannelData BrowserSession()
        {
            return new BrowserSession
            {
                AcceptHeader = "Accept:  *.*, q=0.1",
                JavaEnabled = true,
                JavascriptEnabled = true,
                Language = "FR-fr",
                ColorDepth = "16",
                ScreenWidth = "1920",
                ScreenHeight = "1080",
                Timezone = "60",
                UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36",
                ThreeDsMethodCompletion = ThreeDsMethodCompletion.Y,
                IpAddress = "1.12.123.255"
            };
        }

        protected static ChannelData AppSession()
        {
            var sdkEphemeralPublicKey = new SdkEphemeralPublicKey
            {
                Kty = "EC",
                Crv = "P-256",
                X = "f83OJ3D2xF1Bg8vub9tLe1gHMzV76e8Tus9uPHvRVEU",
                Y = "x_FEzRu9m36HLN_tue659LNpXW6pCyStikYjKIWI5a0"
            };

            var appSession = new AppSession
            {
                SdkAppId = "dbd64fcb-c19a-4728-8849-e3d50bfdde39",
                SdkMaxTimeout = 5L,
                SdkEncryptedData = "{}",
                SdkEphemeralPublicKey = sdkEphemeralPublicKey,
                SdkReferenceNumber = "3DS_LOA_SDK_PPFU_020100_00007",
                SdkTransactionId = "b2385523-a66c-4907-ac3c-91848e8c0067",
                SdkInterfaceType = SdkInterfaceType.Both,
                SdkUIElements = new List<UIElements> {UIElements.SingleSelect, UIElements.HtmlOther}
            };

            return appSession;
        }

        protected static ChannelData MerchantInitiatedSession()
        {
            return new MerchantInitiatedSession { RequestType = RequestType.RecurringTransaction };
        }

        private static bool HasSessionAccepted(SessionResponse obj)
        {
            return obj.Accepted != null &&
                   obj.Accepted.Card != null;
        }

        private static bool HasSessionCreated(SessionResponse obj)
        {
            return obj.Created != null &&
                   obj.Created.Certificates != null &&
                   obj.Created.Ds != null &&
                   obj.Created.Card != null;
        }
    }
}