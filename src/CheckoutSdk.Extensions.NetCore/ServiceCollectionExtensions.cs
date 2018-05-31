using System;
using Checkout;
using Checkout.Extensions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add the Checkout client
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCheckoutSdk(this IServiceCollection services, CheckoutConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton<IHttpClientFactory>(new DefaultHttpClientFactory());
            services.AddSingleton<CheckoutConfiguration>(configuration);
            services.AddSingleton<IApiClient, ApiClient>();

            return services;
        }

        public static IServiceCollection AddCheckoutSdk(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            
            var checkoutOptions = configuration.GetSection("Checkout").Get<CheckoutOptions>();

            var checkoutConfiguration = string.IsNullOrEmpty(checkoutOptions.Uri)
                ? new CheckoutConfiguration(checkoutOptions.SecretKey, checkoutOptions.Sandbox, checkoutOptions.PublicKey)
                : new CheckoutConfiguration(checkoutOptions.SecretKey, checkoutOptions.Uri, checkoutOptions.PublicKey);

            return services.AddCheckoutSdk(checkoutConfiguration);
        }
    }
}