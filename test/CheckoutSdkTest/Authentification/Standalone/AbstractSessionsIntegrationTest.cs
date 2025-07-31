using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.Requests;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.AppChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.AppChannelData.
    SdkEphemPubKey;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.
    MerchantInitiatedChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.HostedCompletion;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.NonHostedCompletion;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ShippingAddress;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.CardSource;
using Checkout.Authentication.Standalone.POSTSessions.Responses;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests.AppRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests.BrowserRequest;
using Checkout.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationType = Checkout.Authentication.Standalone.Common.AuthenticationType;
using ChallengeIndicatorType =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChallengeIndicatorType;
using ThreeDsMethodCompletionType =
    Checkout.Authentication.Standalone.Common.Requests.ThreeDsMethodCompletionType;
using TransactionType = Checkout.Authentication.Standalone.Common.TransactionType;

namespace Checkout.Authentification.Standalone
{
    public abstract class AbstractSessionsIntegrationTest : SandboxTestFixture
    {
        protected AbstractSessionsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        protected async Task<RequestASessionResponse> CreateNonHostedSession(
            AbstractChannelData channelData,
            AuthenticationCategoryType authenticationCategory,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var shippingAddress = new ShippingAddress
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                AddressLine3 = "14 Wells Mews",
                City = "London",
                State = "ENG",
                Country = CountryCode.GB
            };

            var source = new CardSource { Number = "4242424242424242", ExpiryMonth = 6, ExpiryYear = 2030, };

            var sessionRequest = new RequestASessionRequest
            {
                Source = source,
                Amount = 6540L,
                Currency = Currency.GBP,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                AuthenticationCategory = authenticationCategory,
                ChallengeIndicator = challengeIndicator,
                TransactionType = transactionType,
                ShippingAddress = shippingAddress,
                Completion = new NonHostedCompletion { CallbackUrl = "https://merchant.com/callback" },
                ChannelData = channelData,
            };

            return await DefaultApi.AuthenticationClient().RequestASession(sessionRequest, CancellationToken.None);
        }

        protected async Task<RequestASessionResponse> CreateHostedSession()
        {
            var shippingAddress = new ShippingAddress()
            {
                AddressLine1 = "Checkout.com",
                AddressLine2 = "90 Tottenham Court Road",
                AddressLine3 = "14 Wells Mews",
                City = "London",
                State = "ENG",
                Country = CountryCode.GB,
                Zip = "W1T 4TJ"
            };

            var sessionRequest = new RequestASessionRequest
            {
                Source = new CardSource { ExpiryMonth = 1, ExpiryYear = 2030, Number = "4485040371536584" },
                Amount = 100L,
                Currency = Currency.GBP,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                AuthenticationType = AuthenticationType.Regular,
                AuthenticationCategory = AuthenticationCategoryType.Payment,
                ChallengeIndicator = ChallengeIndicatorType.NoPreference,
                Reference = "ORD-5023-4E89",
                TransactionType = TransactionType.GoodsService,
                ShippingAddress = shippingAddress,
                Completion = new HostedCompletion
                {
                    FailureUrl = "https://example.com/sessions/fail",
                    SuccessUrl = "https://example.com/sessions/success",
                }
            };

            return await Retriable(
                async () => await DefaultApi.AuthenticationClient()
                    .RequestASession(sessionRequest, CancellationToken.None),
                HasSessionAccepted);
        }

        protected static AbstractChannelData BrowserChannelData()
        {
            return new BrowserChannelData
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
                ThreeDsMethodCompletion = ThreeDsMethodCompletionType.Y,
                IpAddress = "1.12.123.255"
            };
        }

        protected static AbstractChannelData AppChannelData()
        {
            var sdkEphemPubKey = new SdkEphemPubKey
            {
                Kty = "EC",
                Crv = "P-256",
                X = "f83OJ3D2xF1Bg8vub9tLe1gHMzV76e8Tus9uPHvRVEU",
                Y = "x_FEzRu9m36HLN_tue659LNpXW6pCyStikYjKIWI5a0"
            };

            var appSession = new AppChannelData
            {
                SdkAppId = "dbd64fcb-c19a-4728-8849-e3d50bfdde39",
                SdkMaxTimeout = 5,
                SdkEncryptedData = "{}",
                SdkEphemPubKey = sdkEphemPubKey,
                SdkReferenceNumber = "3DS_LOA_SDK_PPFU_020100_00007",
                SdkTransactionId = "b2385523-a66c-4907-ac3c-91848e8c0067",
                SdkInterfaceType = SdkInterfaceType.Both,
                SdkUiElements =
                    new List<SdkUiElementsType> { SdkUiElementsType.SingleSelect, SdkUiElementsType.HtmlOther }
            };

            return appSession;
        }

        protected static AbstractChannelData MerchantInitiatedChannelData()
        {
            return new MerchantInitiatedChannelData { RequestType = RequestType.RecurringTransaction };
        }

        protected static AbstractUpdateASessionRequest BrowserRequest()
        {
            return new BrowserRequest
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
                ThreeDsMethodCompletion = ThreeDsMethodCompletionType.Y,
                IpAddress = "1.12.123.255"
            };
        }

        protected static AbstractUpdateASessionRequest AppRequest()
        {
            var sdkEphemPubKey =
                new
                    Authentication.Standalone.PUTSessionsIdCollectData.Requests.AppRequest.SdkEphemPubKey.
                    SdkEphemPubKey()
                    {
                        Kty = "EC",
                        Crv = "P-256",
                        X = "f83OJ3D2xF1Bg8vub9tLe1gHMzV76e8Tus9uPHvRVEU",
                        Y = "x_FEzRu9m36HLN_tue659LNpXW6pCyStikYjKIWI5a0"
                    };

            var appSession = new AppRequest()
            {
                SdkAppId = "dbd64fcb-c19a-4728-8849-e3d50bfdde39",
                SdkMaxTimeout = 5,
                SdkEncryptedData = "{}",
                SdkEphemPubKey = sdkEphemPubKey,
                SdkReferenceNumber = "3DS_LOA_SDK_PPFU_020100_00007",
                SdkTransactionId = "b2385523-a66c-4907-ac3c-91848e8c0067",
                SdkInterfaceType =
                    SdkInterfaceType.Both,
                SdkUiElements =
                    new List<SdkUiElementsType>
                    {
                        SdkUiElementsType.SingleSelect,
                        SdkUiElementsType.HtmlOther
                    }
            };

            return appSession;
        }

        private static bool HasSessionAccepted(RequestASessionResponse obj)
        {
            return obj.Accepted != null &&
                   obj.Accepted.Card != null;
        }

        private static bool HasSessionCreated(RequestASessionResponse obj)
        {
            return obj.Created != null &&
                   obj.Created.Certificates != null &&
                   obj.Created.Ds != null &&
                   obj.Created.Card != null;
        }
    }
}