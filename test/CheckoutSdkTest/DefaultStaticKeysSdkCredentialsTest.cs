using System;
using System.Net.Http.Headers;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class DefaultStaticKeysSdkCredentialsTest : UnitTestFixture
    {
        private Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();

        [Fact]
        private void shouldFailCreatingDefaultStaticKeysSdkCredentials_invalidKeys()
        {
            try
            {
                new DefaultStaticKeysSdkCredentials(ValidDefaultSk, InvalidDefaultPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new DefaultStaticKeysSdkCredentials(InvalidDefaultSk, ValidDefaultPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        private void ShouldCreateDefaultStaticKeysSdkCredentials()
        {
            var credentials = new DefaultStaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            credentials.PlatformType.ShouldBe(PlatformType.Default);
            credentials.PublicKey.ShouldBe(ValidDefaultPk);
            credentials.SecretKey.ShouldBe(ValidDefaultSk);
        }

        [Fact]
        private void ShouldCreateDefaultStaticKeysSdkCredentialsForProd()
        {
            const string validDefaultSk = "sk_fde517a8-3f01-41ef-b4bd-4282384b0a64";
            const string validDefaultPk = "pk_fe70ff27-7c32-4ce1-ae90-5691a188ee7b";

            var credentials = new DefaultStaticKeysSdkCredentials(validDefaultSk, validDefaultPk);

            credentials.PlatformType.ShouldBe(PlatformType.Default);
            credentials.PublicKey.ShouldBe(validDefaultPk);
            credentials.SecretKey.ShouldBe(validDefaultSk);
        }

        [Fact]
        private void ShouldFailToCreateDefaultStaticKeysSdkCredentialsForProd()
        {
            var similarDefaultSk = "sk_tost_m73dzbpy7cf3gfd46xr4yj5xo4e";
            var similarDefaultPk = "pk_tost_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            try
            {
                var credentials = new DefaultStaticKeysSdkCredentials(ValidDefaultSk, similarDefaultPk);
                new CheckoutConfiguration(credentials, Environment.Sandbox, httpClientFactoryMock.Object);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                var credentials = new DefaultStaticKeysSdkCredentials(similarDefaultSk, ValidDefaultPk);
                new CheckoutConfiguration(credentials, Environment.Sandbox, httpClientFactoryMock.Object);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        private void ShouldGetAuthorization()
        {
            var credentials = new DefaultStaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);

            var auth1 = credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey);
            auth1.ShouldNotBeNull();
            auth1.GetAuthorizationHeader().ShouldBe(ValidDefaultSk);

            var auth2 = credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey);
            auth2.ShouldNotBeNull();
            auth2.GetAuthorizationHeader().ShouldBe(ValidDefaultPk);
        }
    }
}