using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class CheckoutSdkIntegrationTest
    {
        [Fact]
        public async Task ShouldInstantiateClientWithCustomHttpClientFactory()
        {
            var httpClient = new HttpClient(new CustomMessageHandler());
            httpClient.Timeout = TimeSpan.FromSeconds(2);

            var checkoutApi = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY"))
                .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY"))
                .Environment(Environment.Sandbox)
                .HttpClient(httpClient)
                .Build();

            checkoutApi.ShouldNotBeNull();

            try
            {
                await checkoutApi.EventsClient().RetrieveAllEventTypes();
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                ex.Message.ShouldBe("The API response status code (508) does not indicate success.");
            }
        }
        
        private class CustomMessageHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.LoopDetected;
                response.Content = new StringContent("{}", Encoding.UTF8, "application/json");
                return Task.Factory.StartNew(() => response, cancellationToken);
            }
        }
    }
}