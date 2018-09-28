using Checkout;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add the Checkout client
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class CheckoutServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the default Checkout SDK services to the provided <paramref="serviceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The service collection to add to.</param>
        /// <param name="configuration">The Checkout configuration.</param>
        /// <returns>The service collection with registered Checkout SDK services.</returns>
        public static IServiceCollection AddCheckoutSdk(this IServiceCollection serviceCollection, CheckoutConfiguration configuration)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            serviceCollection.AddSingleton<IHttpClientFactory>(new DefaultHttpClientFactory());
            serviceCollection.AddSingleton<ISerializer>(new JsonSerializer());
            serviceCollection.AddSingleton<CheckoutConfiguration>(configuration);
            serviceCollection.AddSingleton<IApiClient, ApiClient>();
            serviceCollection.AddSingleton<ICheckoutApi, CheckoutApi>();

            return serviceCollection;
        }

        /// <summary>
        /// Registers the default Checkout SDK services to the provided <paramref="serviceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The service collection to add to.</param>
        /// <param name="configuration">The Microsoft configuration used to obtain the Checkout SDK configuration.</param>
        /// <returns>The service collection with registered Checkout SDK services.</returns>
        public static IServiceCollection AddCheckoutSdk(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var checkoutOptions = configuration.GetCheckoutOptions();
            return services.AddCheckoutSdk(checkoutOptions.CreateConfiguration());
        }
    }
}