using Checkout.Previous;
using Moq;
using Shouldly;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class PreviousStaticKeysSdkCredentialsTest : UnitTestFixture
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        private readonly Mock<HttpClient> _httpClientMock = new Mock<HttpClient>();

        [Fact]
        private void shouldFailCreatingPreviousStaticKeysSdkCredentials_invalidKeys()
        {
            try
            {
                new PreviousStaticKeysSdkCredentials(ValidPreviousSk, InvalidPreviousPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new PreviousStaticKeysSdkCredentials(InvalidPreviousSk, ValidPreviousPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        private void ShouldCreatePreviousStaticKeysSdkCredentials()
        {
            var credentials = new PreviousStaticKeysSdkCredentials(ValidPreviousSk, ValidPreviousPk);
            credentials.PlatformType.ShouldBe(PlatformType.Previous);
            credentials.PublicKey.ShouldBe(ValidPreviousPk);
            credentials.SecretKey.ShouldBe(ValidPreviousSk);
        }

        [Fact]
        private void ShouldCreatePreviousStaticKeysSdkCredentialsForProd()
        {
            const string validPreviousSk = "sk_fde517a8-3f01-41ef-b4bd-4282384b0a64";
            const string validPreviousPk = "pk_fe70ff27-7c32-4ce1-ae90-5691a188ee7b";

            var credentials = new PreviousStaticKeysSdkCredentials(validPreviousSk, validPreviousPk);

            credentials.PlatformType.ShouldBe(PlatformType.Previous);
            credentials.PublicKey.ShouldBe(validPreviousPk);
            credentials.SecretKey.ShouldBe(validPreviousSk);
        }

        [Fact]
        private void ShouldFailToCreatePreviousStaticKeysSdkCredentialsForProd()
        {
            var similarPreviousSk = "sk_tost_m73dzbpy7cf3gfd46xr4yj5xo4e";
            var similarPreviousPk = "pk_tost_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            try
            {
                var credentials = new PreviousStaticKeysSdkCredentials(ValidPreviousSk, similarPreviousPk);
                new CheckoutConfiguration(credentials, Environment.Sandbox, _httpClientFactoryMock.Object, _httpClientMock.Object);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                var credentials = new PreviousStaticKeysSdkCredentials(similarPreviousSk, ValidPreviousPk);
                new CheckoutConfiguration(credentials, Environment.Sandbox, _httpClientFactoryMock.Object, _httpClientMock.Object);
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
            var credentials = new PreviousStaticKeysSdkCredentials(ValidPreviousSk, ValidPreviousPk);

            var auth1 = credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey);
            auth1.ShouldNotBeNull();
            auth1.GetAuthorizationHeader().ShouldBe(ValidPreviousSk);

            var auth2 = credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey);
            auth2.ShouldNotBeNull();
            auth2.GetAuthorizationHeader().ShouldBe(ValidPreviousPk);
        }
    }
}