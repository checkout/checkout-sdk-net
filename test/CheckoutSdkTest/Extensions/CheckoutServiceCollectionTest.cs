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
        private void ShouldCreateCheckoutPreviousApiSingleton()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsPreviousTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration, loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<Previous.ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

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
            services.AddCheckoutSdk(configuration, loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

        [Fact]
        private void ShouldCreateCheckoutDefaultOAuthApiSingleton()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactory = new DefaultHttpClientFactory();
            IEnumerable<KeyValuePair<string, string>> credentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Checkout:ClientId",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_ID")),
                new KeyValuePair<string, string>("Checkout:ClientSecret",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET")),
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(credentials)
                .AddJsonFile("./Resources/AppSettingsDefaultOAuthTest.json").Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration, loggerFactoryMock.Object,
                httpClientFactory);

            var serviceProvider = services.BuildServiceProvider();
            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("Checkout")]
        [InlineData("NamedSection")]
        private void ShouldCreateCheckoutPreviousApiSingleton_NamedSection(string sectionName)
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsPreviousTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<Previous.ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

        [Theory]
        [InlineData("Checkout")]
        [InlineData("NamedSection")]
        private void ShouldCreateCheckoutDefaultApiSingleton_NamedSection(string sectionName)
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), loggerFactoryMock.Object,
                httpClientFactoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            httpClientFactoryMock.Verify(mock => mock.CreateClient());
        }

        [Theory]
        [InlineData("Checkout")]
        [InlineData("NamedSection")]
        private void ShouldCreateCheckoutDefaultOAuthApiSingleton_NamedSection(string sectionName)
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var httpClientFactory = new DefaultHttpClientFactory();
            IEnumerable<KeyValuePair<string, string>> credentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>($"{sectionName}:ClientId",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_ID")),
                new KeyValuePair<string, string>($"{sectionName}:ClientSecret",
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET")),
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(credentials)
                .AddJsonFile("./Resources/AppSettingsDefaultOAuthTest.json").Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), loggerFactoryMock.Object,
                httpClientFactory);

            var serviceProvider = services.BuildServiceProvider();
            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();
        }
        
        //TODO add a test where HttpClient is provided by json config
    }
}