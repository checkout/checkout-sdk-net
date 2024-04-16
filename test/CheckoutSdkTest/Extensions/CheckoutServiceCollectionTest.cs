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
    public sealed class CheckoutServiceCollectionTest
    {
        private readonly ILoggerFactory _loggerFactory;
        
        public CheckoutServiceCollectionTest()
        {
            _loggerFactory = new LoggerFactory();
        }
        
        [Fact]
        private void ShouldCreateCheckoutPreviousApiSingleton()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsPreviousTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration, _loggerFactory,
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
            services.AddCheckoutSdk(configuration, _loggerFactory,
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
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsPreviousTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), _loggerFactory,
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
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultTest.json")
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), _loggerFactory,
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
            services.AddCheckoutSdk(configuration.GetRequiredSection(sectionName), _loggerFactory,
                httpClientFactory);

            var serviceProvider = services.BuildServiceProvider();
            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();
        }
    }
}