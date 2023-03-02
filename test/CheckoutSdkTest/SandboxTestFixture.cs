using Checkout.Accounts;
using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Tokens;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog.Extensions.Logging;
using Shouldly;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Checkout
{
    public abstract class SandboxTestFixture
    {
        protected readonly Previous.ICheckoutApi PreviousApi;
        protected readonly ICheckoutApi DefaultApi;
        private readonly ILogger _log;
        private const int TryMaxAttempts = 10;

        protected SandboxTestFixture(PlatformType platformType)
        {
            var logFactory = new NLogLoggerFactory();
            _log = logFactory.CreateLogger(typeof(SandboxTestFixture));
            switch (platformType)
            {
                case PlatformType.Previous:
                    PreviousApi = CheckoutSdk.Builder()
                        .Previous()
                        .StaticKeys()
                        .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY"))
                        .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY"))
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .HttpClientFactory(new DefaultHttpClientFactory())
                        .Build();
                    break;

                case PlatformType.Default:
                    DefaultApi = CheckoutSdk.Builder().StaticKeys()
                        .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PUBLIC_KEY"))
                        .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_SECRET_KEY"))
                        .Environment(Environment.Sandbox)
                        .LogProvider(logFactory)
                        .Build();
                    break;

                case PlatformType.DefaultOAuth:
                    DefaultApi = CheckoutSdk.Builder().OAuth()
                        .ClientCredentials(
                            System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_ID"),
                            System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET"))
                        .Scopes(OAuthScope.Files, OAuthScope.Flow, OAuthScope.Fx, OAuthScope.Gateway,
                            OAuthScope.Accounts, OAuthScope.SessionsApp, OAuthScope.SessionsBrowser,
                            OAuthScope.Vault, OAuthScope.PayoutsBankDetails, OAuthScope.TransfersCreate,
                            OAuthScope.TransfersView, OAuthScope.BalancesView, OAuthScope.VaultCardMetadata,
                            OAuthScope.FinancialActions)
                        .Environment(Environment.Sandbox)
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
                await Task.Delay(2500);
            }

            throw new XunitException("Max attempts reached!");
        }

        protected async Task CheckErrorItem<T>(Func<Task<T>> func, string errorItem)
        {
            try
            {
                T t = await func.Invoke();
                throw new XunitException("Shouldn't get here");
            }
            catch (CheckoutApiException ex)
            {
                ((JArray)ex.ErrorDetails["error_codes"]).ToList().ShouldContain(errorItem);
            }
        }
        
        protected static async Task AssertInvalidDataSent<T>(Task<T> task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                ex.Message.ShouldBe("The API response status code (422) does not indicate success.");
            }
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
            return new Phone { CountryCode = "1", Number = "4155552671" };
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

        protected static AccountHolder GetAccountHolder()
        {
            return new AccountHolder
            {
                FirstName = "John", LastName = "Doe", Phone = GetPhone(), BillingAddress = GetAddress()
            };
        }
        
        protected async Task<CardTokenResponse> RequestToken()
        {
            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();
            return cardTokenResponse;
        }
        
        protected async Task<CreateInstrumentResponse> CreateTokenInstrument(CardTokenResponse token)
        {
            var phone = new Phone {CountryCode = "+1", Number = "415 555 2671"};

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var accountHolder = new AccountHolder
            {
                FirstName = "John", LastName = "Smith", Phone = phone, BillingAddress = billingAddress
            };

            var createTokenInstrumentRequest = new CreateTokenInstrumentRequest
            {
                Token = token.Token, AccountHolder = accountHolder
            };

            var response = await DefaultApi.InstrumentsClient()
                .Create(createTokenInstrumentRequest);
            
            return response;
        }
    }
}