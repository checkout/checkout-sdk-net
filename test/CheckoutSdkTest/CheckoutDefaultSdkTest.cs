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
        private void ShouldCreateStaticKeysCheckoutSdks()
        {
            var checkoutApi1 = CheckoutSdk
                .Builder()
                .StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk
                .Builder()
                .StaticKeys()
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldCreateStaticKeysCheckoutSdksWithLegacyDomain()
        {
#pragma warning disable CS0618 // testing the deprecated legacy-domain opt-out
            var checkoutApi = CheckoutSdk
                .Builder()
                .StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .UseLegacyDomain()
                .Build();
#pragma warning restore CS0618

            checkoutApi.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdkWithoutSubdomainOrLegacyDomain()
        {
            try
            {
                CheckoutSdk
                    .Builder()
                    .StaticKeys()
                    .PublicKey(ValidDefaultPk)
                    .SecretKey(ValidDefaultSk)
                    .Environment(Environment.Sandbox)
                    .Build();
                throw new XunitException();
            }
            catch (CheckoutArgumentException e)
            {
                e.Message.ShouldContain("EnvironmentSubdomain is required");
            }
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdkWithBothSubdomainAndLegacyDomain()
        {
            try
            {
#pragma warning disable CS0618 // testing the deprecated legacy-domain opt-out
                CheckoutSdk
                    .Builder()
                    .StaticKeys()
                    .PublicKey(ValidDefaultPk)
                    .SecretKey(ValidDefaultSk)
                    .Environment(Environment.Sandbox)
                    .EnvironmentSubdomain("1234doma")
                    .UseLegacyDomain()
                    .Build();
#pragma warning restore CS0618
                throw new XunitException();
            }
            catch (CheckoutArgumentException e)
            {
                e.Message.ShouldContain("cannot both be set");
            }
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdkWithInvalidSubdomain()
        {
            try
            {
                CheckoutSdk
                    .Builder()
                    .StaticKeys()
                    .PublicKey(ValidDefaultPk)
                    .SecretKey(ValidDefaultSk)
                    .Environment(Environment.Sandbox)
                    .EnvironmentSubdomain("invalid_subdomain!")
                    .Build();
                throw new XunitException();
            }
            catch (CheckoutArgumentException e)
            {
                e.Message.ShouldContain("invalid environment subdomain");
            }
        }

        [Fact]
        private void ShouldFailToCreateCheckoutSdks()
        {
            try
            {
                CheckoutSdk
                    .Builder()
                    .StaticKeys()
                    .PublicKey(InvalidPreviousPk)
                    .SecretKey(ValidDefaultSk)
                    .Environment(Environment.Sandbox)
                    .EnvironmentSubdomain("1234doma")
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
                    .StaticKeys()
                    .PublicKey(ValidDefaultPk)
                    .SecretKey(InvalidDefaultSk)
                    .Environment(Environment.Sandbox)
                    .EnvironmentSubdomain("1234doma")
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
                .StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .HttpClientFactory(httpClientFactory.Object)
                .Build();

            checkoutApi.ShouldNotBeNull();
            httpClientFactory.Verify(mock => mock.CreateClient());
        }

        [Fact]
        private async void ShouldCreateStaticKeysWithSubdomainCheckoutSdks()
        {
            var checkoutApi1 = CheckoutSdk
                .Builder()
                .StaticKeys()
                .PublicKey(ValidDefaultPk)
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .Build();

            checkoutApi1.ShouldNotBeNull();

            var checkoutApi2 = CheckoutSdk
                .Builder()
                .StaticKeys()
                .SecretKey(ValidDefaultSk)
                .Environment(Environment.Sandbox)
                .EnvironmentSubdomain("1234doma")
                .Build();

            checkoutApi2.ShouldNotBeNull();
        }
    }
}
