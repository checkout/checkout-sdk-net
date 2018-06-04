using System;
using System.IO;
using System.Reflection;
using Checkout.Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using NSpec;
using Serilog;

namespace Checkout.Tests
{
    public class ApiTest : nspec
    {
        private static Lazy<CheckoutConfiguration> _configuration = new Lazy<CheckoutConfiguration>(LoadConfiguration);
        public static CheckoutConfiguration Configuration => _configuration.Value;
        protected ICheckoutApi Api { get; private set; }

        void before_each()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .MinimumLevel.Debug()
                .CreateLogger(); // TODO configure from settings

            Api = new CheckoutApi(new ApiClient(Configuration), Configuration);
        }

        private static CheckoutConfiguration LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", true)
                .AddEnvironmentVariables()
                .Build();

            var options = new CheckoutOptions();
            configuration.Bind("Checkout", options);

            return options.CreateConfiguration();
        }
    }
}