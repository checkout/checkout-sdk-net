using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Xunit.Sdk;
using Moq.Protected;

namespace Checkout
{
    public class CheckoutSdkTelemetryIntegrationTest
    {
        [Fact]
        public async Task ShouldSendTelemetryByDefault()
        {
            Mock<HttpMessageHandler> mockedMessageHandler = new Mock<HttpMessageHandler>();
            mockedMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            })
            .Verifiable();

            var httpFactory = new TestingClientFactory(mockedMessageHandler.Object);

            var checkoutApi = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY"))
                .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY"))
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpFactory)
                .Build();

            checkoutApi.ShouldNotBeNull();

            await checkoutApi.EventsClient().RetrieveAllEventTypes();
            await checkoutApi.EventsClient().RetrieveAllEventTypes();
            await checkoutApi.EventsClient().RetrieveAllEventTypes();

            mockedMessageHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(2), // we expected two sends to contain the telemetry header
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Headers.Contains("cko-sdk-telemetry")
            ),
            ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task ShouldNotSendTelemetryWhenOptedOut()
        {
            Mock<HttpMessageHandler> mockedMessageHandler = new Mock<HttpMessageHandler>();
            mockedMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            })
            .Verifiable();

            var httpFactory = new TestingClientFactory(mockedMessageHandler.Object);

            var checkoutApi = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY"))
                .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY"))
                .RecordTelemetry(false)
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpFactory)
                .Build();

            checkoutApi.ShouldNotBeNull();

            await checkoutApi.EventsClient().RetrieveAllEventTypes();
            await checkoutApi.EventsClient().RetrieveAllEventTypes();

            mockedMessageHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(0), // we expected only one to contain the telemetry header
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Headers.Contains("cko-sdk-telemetry")
            ),
            ItExpr.IsAny<CancellationToken>()
            );
        }

        private class TestingClientFactory : IHttpClientFactory
        {
            HttpMessageHandler _handler;

            public TestingClientFactory(HttpMessageHandler handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient()
            {
                var httpClient = new HttpClient(_handler);
                httpClient.Timeout = TimeSpan.FromSeconds(2);
                return httpClient;
            }
        }
    }
}