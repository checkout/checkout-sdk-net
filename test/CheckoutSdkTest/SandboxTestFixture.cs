using Checkout.Common;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Checkout
{
    public abstract class SandboxTestFixture
    {
        protected readonly ICheckoutApi DefaultApi;
        protected readonly Four.ICheckoutApi FourApi;
        private readonly ILogger _log;
        private const int TryMaxAttempts = 10;

        protected SandboxTestFixture(PlatformType platformType)
        {
            var logFactory = new NLogLoggerFactory();
            _log = logFactory.CreateLogger(typeof(SandboxTestFixture));
            switch (platformType)
            {
                case PlatformType.Default:
                    DefaultApi = CheckoutSdk.DefaultSdk().StaticKeys()
                        .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PUBLIC_KEY"))
                        .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_SECRET_KEY"))
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .HttpClientFactory(new DefaultHttpClientFactory())
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
                            FourOAuthScope.Vault, FourOAuthScope.PayoutsBankDetails, FourOAuthScope.TransfersCreate, FourOAuthScope.BalancesView)
                        .Environment(Environment.Sandbox)
                        .FilesEnvironment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .Build();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(platformType), platformType, null);
            }
        }

        protected static string GenerateRandomEmail()
        {
            return $"{Guid.NewGuid()}@checkout-sdk-net.com";
        }

        protected async Task<T> Retriable<T>(Func<Task<T>> func, Predicate<T> predicate = null)
        {
            var attempts = 1;
            while (attempts <= TryMaxAttempts)
            {
                try
                {
                    T t = await func.Invoke();
                    predicate?.Invoke(t).ShouldBeTrue();
                    return t;
                }
                catch (Exception ex)
                {
                    _log.LogWarning(
                        @"Request/Predicate failed with error '{Error}' - retry {CurrentAttempt}/{MaxAttempts}",
                        ex.Message, attempts, TryMaxAttempts);
                }

                attempts++;
                await Task.Delay(2000);
            }

            throw new XunitException("Max attempts reached!");
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

        protected static Phone GetPhone()
        {
            return new Phone {CountryCode = "1", Number = "4155552671"};
        }

        protected static Address GetAddress()
        {
            return new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };
        }
    }
}