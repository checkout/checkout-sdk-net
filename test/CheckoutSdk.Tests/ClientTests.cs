using System;
using System.IO;
using System.Threading.Tasks;
using Checkout.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSpec;
using Serilog;

namespace Checkout.Tests
{
    public class ClientTests : nspec
    {
        async Task it_can_request_payments()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .MinimumLevel.Debug()
                .CreateLogger();
            
            var api = CheckoutApi.Create("sk_dfee3242-a70d-4903-918d-64395e7adff9", "https://sandbox.checkout.com/api2/");
            
            var cardPaymentRequest = new CardPaymentRequest(
                new CardSource("5436031030606378", 6, 2025),
                1099,
                Currency.USD
            );
            
            var apiResponse = await api.Payments.RequestAsync(cardPaymentRequest);
        }

        // /// <summary>
        // /// Builds a configuration from file and event variable sources
        // /// </summary>
        // /// <returns>The built configuration</returns>
        // private static IConfiguration BuildConfiguration()
        // {
        //     return new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json", optional: true)
        //         .AddEnvironmentVariables()
        //         .Build();
        // }

        // // Use this method to add services to the container.
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     var configuration = BuildConfiguration();
        //     services.AddCheckoutSdk(configuration);
        // }

        // public ClientTests(ICheckoutApi checkoutApi)
        // {
        //     checkoutApi.Payments.RequestAsync(null);
        // }
    }
}