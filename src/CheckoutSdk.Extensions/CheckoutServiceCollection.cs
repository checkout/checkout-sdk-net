using Checkout;
using Checkout.Previous;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using ICheckoutApi = Checkout.Previous.ICheckoutApi;

namespace CheckoutSDK.Extensions.Configuration
{
    public static class CheckoutServiceCollection
    {
        public static IServiceCollection AddCheckoutSdk(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ILoggerFactory loggerFactory = null,
            HttpClient httpClient = null)
        {
            CheckoutUtils.ValidateParams("serviceCollection", serviceCollection, "configuration", configuration);
            var checkoutOptions = configuration.GetCheckoutOptions();

            return AddSdkFromOptions(serviceCollection, checkoutOptions, loggerFactory, httpClient);
        }

        public static IServiceCollection AddCheckoutSdk(
            this IServiceCollection serviceCollection,
            IConfigurationSection configurationSection,
            ILoggerFactory loggerFactory = null,
            HttpClient httpClient = null)
        {
            CheckoutUtils.ValidateParams("serviceCollection", serviceCollection,
                nameof(configurationSection), configurationSection);
            var checkoutOptions = configurationSection.GetCheckoutOptions();

            return AddSdkFromOptions(serviceCollection, checkoutOptions, loggerFactory, httpClient);
        }

        private static IServiceCollection AddSdkFromOptions(
            IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions,
            ILoggerFactory loggerFactory = null,
            HttpClient httpClient = null)
        {
            if (checkoutOptions == null)
            {
                throw new CheckoutArgumentException("Checkout options was not initialized correctly");
            }

            switch (checkoutOptions.PlatformType)
            {
                case PlatformType.Previous:
                    return AddSingletonPreviousSdk(serviceCollection, checkoutOptions, loggerFactory,
                        httpClient);
                case PlatformType.Default:
                    return AddSingletonDefaultSdk(serviceCollection, checkoutOptions, loggerFactory,
                        httpClient);
                case PlatformType.DefaultOAuth:
                    return AddSingletonDefaultOAuthSdk(serviceCollection, checkoutOptions, loggerFactory,
                        httpClient);
                default:
                    throw new CheckoutArgumentException($"Unsupported PlatformType:{checkoutOptions.PlatformType}");
            }
        }

        private static IServiceCollection AddSingletonPreviousSdk(
            IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions,
            ILoggerFactory loggerFactory,
            HttpClient httpClient)
        {
            var checkoutSdkBuilder = CheckoutSdk.Builder()
                .Previous()
                .StaticKeys()
                .SecretKey(checkoutOptions.SecretKey)
                .PublicKey(checkoutOptions.PublicKey);
            SetCommonAttributes<CheckoutPreviousSdk.CheckoutStaticKeysSdkBuilder, ICheckoutApi>(
                checkoutSdkBuilder,
                checkoutOptions, loggerFactory, httpClient);
            return serviceCollection.AddSingleton(checkoutSdkBuilder.Build());
        }

        private static IServiceCollection AddSingletonDefaultSdk(
            IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions,
            ILoggerFactory loggerFactory,
            HttpClient httpClient)
        {
            var checkoutSdkBuilder = CheckoutSdk.Builder()
                .StaticKeys()
                .SecretKey(checkoutOptions.SecretKey)
                .PublicKey(checkoutOptions.PublicKey);
            SetCommonAttributes<CheckoutSdkBuilder.CheckoutStaticKeysSdkBuilder, Checkout.ICheckoutApi>(
                checkoutSdkBuilder, checkoutOptions, loggerFactory, httpClient);
            return serviceCollection.AddSingleton(checkoutSdkBuilder.Build());
        }

        private static IServiceCollection AddSingletonDefaultOAuthSdk(
            IServiceCollection serviceCollection,
            CheckoutOptions checkoutOptions,
            ILoggerFactory loggerFactory,
            HttpClient httpClient)
        {
            var checkoutSdkBuilder = CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(checkoutOptions.ClientId, checkoutOptions.ClientSecret);
            if (checkoutOptions.AuthorizationUri != null)
            {
                checkoutSdkBuilder.AuthorizationUri(checkoutOptions.AuthorizationUri);
            }

            if (checkoutOptions.Scopes != null)
            {
                checkoutSdkBuilder.Scopes(checkoutOptions.Scopes);
            }

            SetCommonAttributes<CheckoutSdkBuilder.CheckoutOAuthSdkBuilder, Checkout.ICheckoutApi>(
                checkoutSdkBuilder, checkoutOptions, loggerFactory, httpClient);
            return serviceCollection.AddSingleton(checkoutSdkBuilder.Build());
        }

        private static void SetCommonAttributes<TB, TC>(
            TB builder,
            CheckoutOptions options,
            ILoggerFactory loggerFactory,
            HttpClient httpClient)
            where TB : AbstractCheckoutSdkBuilder<TC>
            where TC : ICheckoutApiClient
        {
            builder.Environment(options.Environment);
            if (loggerFactory != null)
            {
                builder.LogProvider(loggerFactory);
            }

            if (httpClient != null)
            {
                builder.HttpClient(httpClient);
            }
        }
    }
}