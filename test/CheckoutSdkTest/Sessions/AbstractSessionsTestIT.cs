using Checkout.Common;
using Checkout.Sessions.Channel;
using Checkout.Sessions.Completion;
using Checkout.Sessions.Source;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sessions
{
    public abstract class AbstractSessionsTestIT : SandboxTestFixture
    {
        public AbstractSessionsTestIT() : base(PlatformType.FourOAuth)
        {
        }

        protected async Task<SessionResponse> CreateNonHostedSession(ChannelData channelData,
                                                     Category authenticationCategory,
                                                     ChallengeIndicator challengeIndicator,
                                                     TransactionType transactionType)
        {
            var sessionRequest = new SessionRequest();
            var billingAddress = new SessionAddress
            (
                "Address Line 1",
                "Address Line 2",
                "Address Line 3",
                "city",
                string.Empty,
                string.Empty,
                country: Common.CountryCode.GB
            );

            var source = new SessionCardSource
            (
                TestCardSource.Visa.Number.ToString(),
                TestCardSource.Visa.ExpiryMonth,
                TestCardSource.Visa.ExpiryYear,
                "John Doe",
                GenerateRandomEmail(),
                billingAddress,
                GetPhone(),
                GetPhone(),
                GetPhone()
            );

            var shippingAddress = new SessionAddress
            {
                AddressLine1 = "Checkout.com",
                AddressLine2 = "ABC building",
                AddressLine3 = "14 Wells Mews",
                City = "London",
                Country = CountryCode.GB,
                State = "ENG",
                Zip = "W1T 4TJ"
            };

            sessionRequest.Source = source;
            sessionRequest.Amount = 6540L;
            sessionRequest.Currency = Currency.USD;
            sessionRequest.ProcessingChannelId = "pc_5jp2az55l3cuths25t5p3xhwru";
            sessionRequest.Marketplace = new MarketplaceData() { SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u" };
            sessionRequest.AuthenticationCategory = authenticationCategory;
            sessionRequest.ChallengeIndicator = challengeIndicator;
            sessionRequest.BillingDescriptor = new SessionsBillingDescriptor() { Name = "SUPERHEROES.COM" };
            sessionRequest.Reference = "ORD-5023-4E89";
            sessionRequest.TransactionType = transactionType;
            sessionRequest.ShippingAddress = shippingAddress;
            sessionRequest.Completion = new NonHostedCompletionInfo("https://merchant.com/callback");
            sessionRequest.ChannelData = channelData;

            return await FourApi.SessionsClient().RequestSession(sessionRequest, CancellationToken.None);
        }

        protected async Task<SessionResponse> CreateHostedSession()
        {
            var sessionRequest = new SessionRequest();
            var source = new SessionCardSource()
            {
                ExpiryMonth = 1,
                ExpiryYear = 2030,
                Number = "4485040371536584"
            };

            var shippingAddress = new SessionAddress()
            {
                AddressLine1 = "Checkout.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "ENG",
                Country = CountryCode.GB,
                Zip = "W1T 4TJ"
            };

            var completition = new HostedCompletionInfo(string.Empty, "http://example.com/sessions/success", "http://example.com/sessions/fail");

            sessionRequest.Source = source;
            sessionRequest.Amount = 100L;
            sessionRequest.Currency = Currency.USD;
            sessionRequest.ProcessingChannelId = "pc_5jp2az55l3cuths25t5p3xhwru";
            sessionRequest.AuthenticationType = AuthenticationType.Regular;
            sessionRequest.AuthenticationCategory = Category.Payment;
            sessionRequest.ChallengeIndicator = ChallengeIndicator.NoPreference;
            sessionRequest.Reference = "ORD-5023-4E89";
            sessionRequest.TransactionType = TransactionType.GoodsService;
            sessionRequest.ShippingAddress = shippingAddress;
            sessionRequest.Completion = completition;

            return await FourApi.SessionsClient().RequestSession(sessionRequest, CancellationToken.None);
        }

        protected static Phone GetPhone()
        {
            return new Phone()
            {
                CountryCode = "234",
                Number = "0204567895"
            };
        }

        protected static ChannelData BrowserSession()
        {
            return new BrowserSession()
            {
                AcceptHeader = "Accept:  *.*, q=0.1",
                JavaEnabled = true,
                Language = "FR-fr",
                ColorDepth = "16",
                ScreenWidth = "1920",
                ScreenHeight = "1080",
                Timezone = "60",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36",
                ThreeDsMethodCompletion = ThreeDsMethodCompletion.Y,
                IpAddress = "1.12.123.255"
            };
        }

        protected static ChannelData AppSession()
        {
            var sdkEphemeralPublicKey = new SdkEphemeralPublicKey()
            {
                Kty = "EC",
                Crv = "P-256",
                X = "f83OJ3D2xF1Bg8vub9tLe1gHMzV76e8Tus9uPHvRVEU",
                Y = "x_FEzRu9m36HLN_tue659LNpXW6pCyStikYjKIWI5a0"
            };

            var appSession = new AppSession()
            {
                SdkAppId = "dbd64fcb-c19a-4728-8849-e3d50bfdde39",
                SdkMaxTimeout = 5L,
                SdkEncryptedData = "{}",
                SdkEphemeralPublicKey = sdkEphemeralPublicKey,
                SdkReferenceNumber = "3DS_LOA_SDK_PPFU_020100_00007",
                SdkTransactionId = "b2385523-a66c-4907-ac3c-91848e8c0067",
                SdkInterfaceType = SdkInterfaceType.Both,
                SdkUIElements = new List<UIElements>() { UIElements.SingleSelect, UIElements.HtmlOther }
            };

            return appSession;
        }
    }
}