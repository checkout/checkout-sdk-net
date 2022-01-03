using System;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using Shouldly;

namespace Checkout
{
    public abstract class SandboxTestFixture
    {
        protected readonly ICheckoutApi DefaultApi;
        protected readonly Four.ICheckoutApi FourApi;

        protected SandboxTestFixture(PlatformType platformType)
        {
            var logFactory = new NLogLoggerFactory();
            switch (platformType)
            {
                case PlatformType.Default:
                    DefaultApi = CheckoutSdk.DefaultSdk().StaticKeys()
                        .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PUBLIC_KEY"))
                        .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_SECRET_KEY"))
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .Build();
                    break;
                case PlatformType.Four:
                    FourApi = CheckoutSdk.FourSdk().StaticKeys()
                        .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_PUBLIC_KEY"))
                        .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_SECRET_KEY"))
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .Build();
                    break;
                case PlatformType.FourOAuth:
                    FourApi = CheckoutSdk.FourSdk().OAuth()
                        .ClientCredentials(System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_CLIENT_ID"),
                            System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_CLIENT_SECRET"))
                        .Scopes(FourOAuthScope.Files, FourOAuthScope.Flow, FourOAuthScope.Fx, FourOAuthScope.Gateway,
                            FourOAuthScope.Marketplace, FourOAuthScope.SessionsApp, FourOAuthScope.SessionsBrowser,
                            FourOAuthScope.Vault, FourOAuthScope.PayoutsBankDetails)
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .Build();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(platformType), platformType, null);
            }
        }

        protected static async Task Nap()
        {
            await Task.Delay(2000);
        }

        protected static async Task Nap(int seconds)
        {
            await Task.Delay(seconds * 1000);
        }

        protected static string GenerateRandomEmail()
        {
            return $"{Guid.NewGuid()}@checkout-sdk-net.com";
        }

        protected static async Task AssertNotFound<T>(Task<T> task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                ex.Message.ShouldBe("The API response status code (404) does not indicate success.");
            }
        }
    }
}