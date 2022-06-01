using Moq;
using Shouldly;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class CheckoutFourSdkTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateStaticKeysCheckoutSdks()
        {
            var checkoutApi1 = CheckoutSdk.FourSdk().StaticKeys()
                .PublicKey(ValidFourPk)
                .SecretKey(ValidFourSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk.FourSdk().StaticKeys()
                .SecretKey(ValidFourSk)
                .Environment(Environment.Sandbox)
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdks()
        {
            try
            {
                CheckoutSdk.FourSdk().StaticKeys()
                    .PublicKey(InvalidDefaultPk)
                    .SecretKey(ValidFourSk)
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
                CheckoutSdk.FourSdk().StaticKeys()
                    .PublicKey(ValidFourPk)
                    .SecretKey(InvalidFourSk)
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

            var checkoutApi = CheckoutSdk.FourSdk().StaticKeys()
                .PublicKey(ValidFourPk)
                .SecretKey(ValidFourSk)
                .Environment(Environment.Sandbox)
                .HttpClientFactory(httpClientFactory.Object)
                .Build();

            checkoutApi.ShouldNotBeNull();
            httpClientFactory.Verify(mock => mock.CreateClient());
        }
    }
}