using Checkout.Customers;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout
{
    [Collection("NonParallel")]
    public class ApiClientLoggingTest
    {
        [Theory]
        [InlineData("payments/pay_x/voids", "payments/pay_x/voids")]
        [InlineData("payments?limit=1&reference=ref_1", "payments")]
        [InlineData("customers/a%2Bb%40example.com", "customers/***")]
        [InlineData("customers/a@example.com/x", "customers/***/x")]
        [InlineData("event-types?version=1.0", "event-types")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ShouldLogThePathWithoutQueryAndEmails(string path, string expected)
        {
            ApiClient.LoggablePath(path).ShouldBe(expected);
        }

        [Fact]
        public async Task ShouldLogRequestsWithoutQueryAndEmails()
        {
            var factory = new CapturingLoggerFactory();
            LogProvider.SetLogFactory(factory);
            try
            {
                var httpClientFactory = new Mock<IHttpClientFactory>();
                httpClientFactory.Setup(f => f.CreateClient()).Returns(() => new HttpClient(new OkHandler()));
                var apiClient = new ApiClient(httpClientFactory.Object, new Uri("https://api.sandbox.checkout.com/"),
                    false);
                var customersClient = new CustomersClient(apiClient, Configuration(httpClientFactory.Object));

                await customersClient.Get("log.check+1@example.com");

                var lines = factory.Lines.Where(l => l.Contains("customers")).ToList();
                lines.ShouldHaveSingleItem().ShouldBe("GET: customers/***");
                factory.Lines.ShouldNotContain(l => l.Contains("log.check"));
            }
            finally
            {
                LogProvider.SetLogFactory(TestLoggerFactoryHelper.Instance);
            }
        }

        private static CheckoutConfiguration Configuration(IHttpClientFactory httpClientFactory)
        {
            var credentials = new Mock<SdkCredentials>(PlatformType.Default);
            credentials.Setup(c => c.GetSdkAuthorization(It.IsAny<SdkAuthorizationType>()))
                .Returns(new SdkAuthorization(PlatformType.Default, "sk_sbox_x"));
            return new CheckoutConfiguration(credentials.Object, Environment.Sandbox, httpClientFactory);
        }

        private sealed class OkHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                });
            }
        }

        private sealed class CapturingLoggerFactory : ILoggerFactory
        {
            public ConcurrentQueue<string> Lines { get; } = new ConcurrentQueue<string>();

            public ILogger CreateLogger(string categoryName) => new CapturingLogger(Lines);

            public void AddProvider(ILoggerProvider provider)
            {
            }

            public void Dispose()
            {
            }
        }

        private sealed class CapturingLogger : ILogger
        {
            private readonly ConcurrentQueue<string> _lines;

            public CapturingLogger(ConcurrentQueue<string> lines)
            {
                _lines = lines;
            }

            public IDisposable BeginScope<TState>(TState state) => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter)
            {
                _lines.Enqueue(formatter(state, exception));
            }
        }
    }
}
