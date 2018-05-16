using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout;
using Checkout.Payments;

namespace Payments.Tests
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        async Task PaymentsClientFullDemo()
        {
            var configuration = new CheckoutConfiguration("sk_dfee3242-a70d-4903-918d-64395e7adff9");
            var apiClient = new ApiClient(configuration);
            var paymentsClient = new PaymentsClient(apiClient);

            var apiResponse = await paymentsClient.RequestAsync(new CardPaymentRequest(100, Currency.GBP));

            if (apiResponse.Error != null)
            {
                var approved = apiResponse.Result.Approved;
            }
        }

        async Task PaymentsClientSugarDemo()
        {
            var paymentsClient = PaymentsClient.Create("sk_dfee3242-a70d-4903-918d-64395e7adff9");
            
            var apiResponse = await paymentsClient.RequestAsync(new CardPaymentRequest(100, Currency.GBP));

            // etc.
        }

        async Task PaymentsClientFullControl()
        {
            var httpClient = new HttpClient();
            var httpClientFactory = new DefaultHttpClientFactory(httpClient);
            var configuration = new CheckoutConfiguration("sk_dfee3242-a70d-4903-918d-64395e7adff9");
            var apiClient = new ApiClient(configuration, httpClientFactory);

            var paymentsClient = new PaymentsClient(apiClient);
            
            var apiResponse = await paymentsClient.RequestAsync(new CardPaymentRequest(100, Currency.USD));
            // etc.
        }

        async Task CheckoutApiDemo()
        {
            var api = CheckoutApi.Create("sk_dfee3242-a70d-4903-918d-64395e7adff9");
            
            var apiResponse = await api.Payments.RequestAsync(new CardPaymentRequest(100, Currency.USD));

            // etc.
        }
    }
}
