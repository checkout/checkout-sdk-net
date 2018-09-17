using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using Xunit.Abstractions;

namespace Checkout.Tests
{
    public class ApiTestFixture
    {
        private static readonly Lazy<CheckoutConfiguration> LazyConfiguration = new Lazy<CheckoutConfiguration>(LoadConfiguration);
        public static CheckoutConfiguration Configuration => LazyConfiguration.Value;

        public ApiTestFixture()
        {
            Api = new CheckoutApi(new ApiClient(Configuration), Configuration);
        }

        public void CaptureLogsInTestOutput(ITestOutputHelper outputHelper)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestOutput(outputHelper)
                .MinimumLevel.Debug()
                .CreateLogger();
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