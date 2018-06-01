using NSpec;
using Serilog;

namespace Checkout.Tests
{
    class ApiTest : nspec
    {
        protected ICheckoutApi Api { get; private set; }
        
        void before_each()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .MinimumLevel.Debug()
                .CreateLogger(); // TODO configure from settings
            
            Api = CheckoutApi.Create(
                "sk_dfee3242-a70d-4903-918d-64395e7adff9",
                "https://sandbox.checkout.com/api2/"
            );
        }
    }
}