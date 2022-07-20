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
            var checkoutApi1 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(ValidPreviousPk)
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdks()
        {
            try
            {
                CheckoutSdk.Builder()
                    .Previous()
                    .StaticKeys()
                    .PublicKey(InvalidPreviousPk)
                    .SecretKey(ValidPreviousSk)
                    .Environment(Environment.Sandbox)
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
                CheckoutSdk
                    .Builder()
                    .Previous()
                    .StaticKeys()
                    .PublicKey(ValidPreviousPk)
                    .SecretKey(InvalidPreviousSk)
                    .Environment(Environment.Sandbox)
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

            var checkoutApi = CheckoutSdk
                .Builder()
                .Previous()
                .StaticKeys()
                .PublicKey(ValidPreviousPk)
                .SecretKey(ValidPreviousSk)
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpClientFactory.Object)
                .Build();

            checkoutApi.ShouldNotBeNull();
            httpClientFactory.Verify(mock => mock.CreateClient());
        }
    }
}