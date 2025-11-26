using Moq;
using Shouldly;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class CheckoutPreviousSdkTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateCheckoutSdks()
        {
            var logFactory = CreateLoggerFactory();
            var checkoutApi1 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(ValidPreviousPk)
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .LogProvider(logFactory)
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .LogProvider(logFactory)
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdks()
        {
            try
            {
                var logFactory = CreateLoggerFactory();
                CheckoutSdk.Builder()
                    .Previous()
                    .StaticKeys()
                    .PublicKey(InvalidPreviousPk)
                    .SecretKey(ValidPreviousSk)
                    .Environment(Environment.Sandbox)
                    .LogProvider(logFactory)
                    .Build();
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                var logFactory = CreateLoggerFactory();
                CheckoutSdk
                    .Builder()
                    .Previous()
                    .StaticKeys()
                    .PublicKey(ValidPreviousPk)
                    .SecretKey(InvalidPreviousSk)
                    .Environment(Environment.Sandbox)
                    .LogProvider(logFactory)
                    .Build();
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        public void ShouldInstantiateClientWithCustomHttpClientFactory()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();

            httpClientFactory.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());

            var logFactory = CreateLoggerFactory();
            var checkoutApi = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(ValidPreviousPk)
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpClientFactory.Object)
                .LogProvider(logFactory)
                .Build();

            checkoutApi.ShouldNotBeNull();
            httpClientFactory.Verify(mock => mock.CreateClient());
        }
        
        [Fact]
        private void ShouldCreateCheckoutSdksWithSubdomain()
        {
            var logFactory = CreateLoggerFactory();
            var checkoutApi1 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(ValidPreviousPk)
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .LogProvider(logFactory)
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var logFactory2 = CreateLoggerFactory();
            var checkoutApi2 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .LogProvider(logFactory2)
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }
    }
}