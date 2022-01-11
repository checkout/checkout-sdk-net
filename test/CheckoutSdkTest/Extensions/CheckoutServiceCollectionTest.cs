using CheckoutSDK.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Checkout.Extensions
{
    public class CheckoutServiceCollectionTest
    {
        [Fact]
        private void ShouldCreateCheckoutDefaultApiSingleton()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            CheckoutServiceCollection.AddCheckoutSdk(services, configuration, loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

        [Fact]
        private void ShouldCreateCheckoutFourApiSingleton()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsFourTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            CheckoutServiceCollection.AddCheckoutSdk(services, configuration, loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<Four.ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

        [Fact]
        private void ShouldCreateCheckoutFourOAuthApiSingleton()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactory = new DefaultHttpClientFactory();
            IEnumerable<KeyValuePair<string, string>> credentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Checkout:ClientId",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_CLIENT_ID")),
                new KeyValuePair<string, string>("Checkout:ClientSecret",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_OAUTH_CLIENT_SECRET")),
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(credentials)
                .AddJsonFile("./Resources/AppSettingsFourOAuthTest.json").Build();

            IServiceCollection services = new ServiceCollection();
            CheckoutServiceCollection.AddCheckoutSdk(services, configuration, loggerFactoryMock.Object,
                httpClientFactory);

            var serviceProvider = services.BuildServiceProvider();
            var checkoutApi = serviceProvider.GetService<Four.ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();
        }
    }
}