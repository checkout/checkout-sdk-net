using Moq;
using Shouldly;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class CheckoutDefaultSdkTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateCheckoutSdks()
        {
            var checkoutApi1 = CheckoutSdk.DefaultSdk().StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk.DefaultSdk().StaticKeys()
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdks()
        {
            try
            {
                CheckoutSdk.DefaultSdk().StaticKeys()
                    .PublicKey(InvalidDefaultPk)
                    .SecretKey(ValidDefaultSk)
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
                CheckoutSdk.DefaultSdk().StaticKeys()
                    .PublicKey(ValidDefaultPk)
                    .SecretKey(InvalidDefaultSk)
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

            var checkoutApi = CheckoutSdk.DefaultSdk().StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpClientFactory.Object)
                .Build();

            checkoutApi.ShouldNotBeNull();
            httpClientFactory.Verify(mock => mock.CreateClient());
        }
    }
}