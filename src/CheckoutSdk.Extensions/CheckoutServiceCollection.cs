using Checkout;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CheckoutSDK.Extensions.Configuration
{
    public static class CheckoutServiceCollection
    {
        public static IServiceCollection AddCheckoutSdk(IServiceCollection serviceCollection,
            IConfiguration configuration,
            ILoggerFactory loggerFactory = null,
            IHttpClientFactory httpClientFactory = null)
        {
            CheckoutUtils.ValidateParams("serviceCollection", serviceCollection, "configuration", configuration);
            var checkoutOptions = configuration.GetCheckoutOptions();
            if (checkoutOptions == null)
            {
                throw new CheckoutArgumentException("Checkout options was not initialized correctly");
            }

            switch (checkoutOptions.PlatformType)
            {
                case PlatformType.Default:
                    return AddSingletonDefaultSdk(serviceCollection, checkoutOptions, loggerFactory, httpClientFactory);
                case PlatformType.Four:
                    return AddSingletonFourSdk(serviceCollection, checkoutOptions, loggerFactory, httpClientFactory);
                case PlatformType.FourOAuth:
                    return AddSingletonFourOAuthSdk(serviceCollection, checkoutOptions, loggerFactory,
                        httpClientFactory);
                default:
                    throw new CheckoutArgumentException($"Unsupported PlatformType:{checkoutOptions.PlatformType}");
            }
        }

        private static IServiceCollection AddSingletonDefaultSdk(IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            var defaultBuilder = CheckoutSdk.DefaultSdk().StaticKeys()
                .SecretKey(checkoutOptions.SecretKey)
                .PublicKey(checkoutOptions.PublicKey);
            SetCommonAttributes<CheckoutDefaultSdk.StaticKeysCheckoutSdkBuilder, ICheckoutApi>(defaultBuilder,
                checkoutOptions, loggerFactory, httpClientFactory);
            return serviceCollection.AddSingleton(defaultBuilder.Build());
        }

        private static IServiceCollection AddSingletonFourSdk(IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            var fourBuilder = CheckoutSdk.FourSdk().StaticKeys()
                .SecretKey(checkoutOptions.SecretKey)
                .PublicKey(checkoutOptions.PublicKey);
            SetCommonAttributes<CheckoutFourSdk.FourStaticKeysCheckoutSdkBuilder, Checkout.Four.ICheckoutApi>(
                fourBuilder, checkoutOptions, loggerFactory, httpClientFactory);
            return serviceCollection.AddSingleton(fourBuilder.Build());
        }

        private static IServiceCollection AddSingletonFourOAuthSdk(IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            var fourOAuthBuilder = CheckoutSdk.FourSdk().OAuth()
                .ClientCredentials(checkoutOptions.ClientId, checkoutOptions.ClientSecret);
            if (checkoutOptions.AuthorizationUri != null)
            {
                fourOAuthBuilder.AuthorizationUri(checkoutOptions.AuthorizationUri);
            }

            if (checkoutOptions.Scopes != null)
            {
                fourOAuthBuilder.Scopes(checkoutOptions.Scopes);
            }

            SetCommonAttributes<CheckoutFourSdk.FourOAuthCheckoutSdkBuilder, Checkout.Four.ICheckoutApi>(
                fourOAuthBuilder, checkoutOptions, loggerFactory, httpClientFactory);
            return serviceCollection.AddSingleton(fourOAuthBuilder.Build());
        }

        private static void SetCommonAttributes<TB, TC>(TB builder, CheckoutOptions options,
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory) where TB : AbstractCheckoutSdkBuilder<TC>
            where TC : ICheckoutApiClient
        {
            builder.Environment(options.Environment);
            if (loggerFactory != null)
            {
                builder.LogProvider(loggerFactory);
            }

            if (httpClientFactory != null)
            {
                builder.HttpClientFactory(httpClientFactory);
            }
        }
    }
}