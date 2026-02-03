using Checkout.Payments.Request;
using Checkout.Payments.Response;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class SearchPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        public SearchPaymentsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "Avoid because can create timeout in the pipeline, activate when needed")]
        private async Task ShouldSearchPayments()
        {
            var paymentResponse = await MakeCardPayment(true);

            var searchRequest = new PaymentsSearchRequest
            {
                Query = $"id:'{paymentResponse.Id}'",
                Limit = 10,
                From = DateTime.UtcNow.AddMinutes(-5),
                To = DateTime.UtcNow.AddMinutes(5)
            };

            PaymentsQueryResponse response = await Retriable(async () =>
                await DefaultApi.PaymentsClient().SearchPayments(searchRequest), SearchHasResults);

            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.Data.Count.ShouldBeGreaterThan(0);
            response.Data[0].Id.ShouldBe(paymentResponse.Id);
        }

        private static bool SearchHasResults(PaymentsQueryResponse response)
        {
            return response?.Data != null && response.Data.Count > 0;
        }
    }
}
