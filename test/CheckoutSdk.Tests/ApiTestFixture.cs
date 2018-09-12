using System;
using System.IO;
using System.Reflection;
using Checkout.Sdk.Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Checkout.Sdk.Tests
{
    public class ApiTestFixture
    {
        private static readonly Lazy<CheckoutConfiguration> LazyConfiguration = new Lazy<CheckoutConfiguration>(LoadConfiguration);
        public static CheckoutConfiguration Configuration => LazyConfiguration.Value;

        public ApiTestFixture()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .MinimumLevel.Debug()
                .CreateLogger(); // TODO configure from settings

            Api = new CheckoutApi(new ApiClient(Configuration), Configuration);
        }

        public ICheckoutApi Api { get; private set; }

        private static CheckoutConfiguration LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", true)
                .AddEnvironmentVariables()
                .Build();

            var options = configuration.GetCheckoutOptions();
            return options.CreateConfiguration();
        }
    }
}