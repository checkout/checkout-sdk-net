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
                    var defaultBuilder = CheckoutSdk.DefaultSdk().StaticKeys()
                        .SecretKey(checkoutOptions.SecretKey)
                        .PublicKey(checkoutOptions.PublicKey)
                        .Environment(checkoutOptions.Environment);
                    if (loggerFactory != null)
                    {
                        defaultBuilder.LogProvider(loggerFactory);
                    }

                    if (httpClientFactory != null)
                    {
                        defaultBuilder.HttpClientFactory(httpClientFactory);
                    }

                    return serviceCollection.AddSingleton(defaultBuilder.Build());
                case PlatformType.Four:
                    var fourBuilder = CheckoutSdk.FourSdk().StaticKeys()
                        .SecretKey(checkoutOptions.SecretKey)
                        .PublicKey(checkoutOptions.PublicKey)
                        .Environment(checkoutOptions.Environment);
                    if (loggerFactory != null)
                    {
                        fourBuilder.LogProvider(loggerFactory);
                    }

                    if (httpClientFactory != null)
                    {
                        fourBuilder.HttpClientFactory(httpClientFactory);
                    }

                    return serviceCollection.AddSingleton(fourBuilder.Build());
                default:
                    throw new CheckoutArgumentException($"Unsupported PlatformType:{checkoutOptions.PlatformType}");
            }
        }
    }
}