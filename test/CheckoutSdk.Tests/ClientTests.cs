using System;
using System.IO;
using System.Threading.Tasks;
using Checkout.Payments;
using Checkout.Payments.Request;
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

            var cardPaymentRequest = new PaymentRequest<CardSource>(
                new CardSource("5436031030606378", 6, 2025),
                1099,
                Currency.USD
            )
            {
                Customer = new Customer
                {
                    Id = "cus_xxx"
                }
            };


            // 3. Using generic union type
            var apiResponse = await api.Payments.RequestAsync(cardPaymentRequest);

            if (apiResponse.Result.RequiresRedirect)
            {
                var paymentId = apiResponse.Result.Accepted.Id;
            }
            else
            {
                var approved = apiResponse.Result.Processed.Approved;
            }


        }
    }
}