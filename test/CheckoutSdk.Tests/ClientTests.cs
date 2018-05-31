using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Checkout.Payments;
using Checkout.Payments.Request;
using Checkout.Tokens;
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

            var api = CheckoutApi.Create(
                "sk_dfee3242-a70d-4903-918d-64395e7adff9", 
                "https://sandbox.checkout.com/api2/",
                "pk_xxx"  // public key only needs to be set for tokenisation
            );

            var tokenApiResponse = await api.Tokens.RequestAsync(
                new CardTokenRequest(
                    "5436031030606378",
                    6,
                    2025
                )
            );

            var paymentApiResponse = await api.Payments.RequestAsync(
                new PaymentRequest<TokenSource>(
                    new TokenSource(tokenApiResponse.Result.Token),
                    Currency.GBP,
                    100
                )
            );

            if (paymentApiResponse.HasError)
            {
                ShowError(paymentApiResponse.Error.ErrorCodes);
                return;
            }

            if (paymentApiResponse.Result.IsAccepted)
            {
                var accepted = paymentApiResponse.Result.Accepted;

                if (accepted.RequiresRedirect())
                {
                    RedirectCustomer(accepted.GetRedirectLink().Href);
                    return;
                }
            }
            
            // otherwise payment was processed

            var processed = paymentApiResponse.Result.Processed;

            if (processed.Approved)
            {
                if (processed.Risk.Flagged)
                {
                    // need to capture manually
                }
            }

            // 3. Using generic union type
            var apiResponse = await api.Payments.RequestAsync(cardPaymentRequest);

            if (apiResponse.Data.RequiresRedirect)
            {
                var paymentId = apiResponse.Data.Accepted.Id;
            }
            else
            {
                var approved = apiResponse.Data.Processed.Approved;
            }


        }

        private void RedirectCustomer(string href)
        {
            throw new NotImplementedException();
        }

        private void ShowError(IEnumerable<string> errorCodes)
        {
            throw new NotImplementedException();
        }
    }
}