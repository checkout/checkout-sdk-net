using Shouldly;
using System;
using Xunit;
using Xunit.Sdk;

namespace Checkout.Sessions
{
    public class SessionSecretSdkCredentialsTest
    {
        [Fact]
        private void ShouldFailCreatingSessionSecretSdkCredentialsInvalidSecret()
        {
            try
            {
                new SessionSecretSdkCredentials(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("secret cannot be null");
            }
        }

        [Fact]
        private void ShouldCreateSessionSecretSdkCredentials()
        {
            var credentials = new SessionSecretSdkCredentials("test");
            credentials.PlatformType.ShouldBe(PlatformType.Custom);
            credentials.Secret.ShouldBe("test");
        }

        [Fact]
        private void ShouldGetAuthorization()
        {
            var credentials = new SessionSecretSdkCredentials("test");
            var auth = credentials.GetSdkAuthorization(SdkAuthorizationType.Custom);
            auth.ShouldNotBeNull();
            auth.GetAuthorizationHeader().ShouldBe("test");
        }

        [Fact]
        private void ShouldNotGetAuthorization()
        {
            var credentials = new SessionSecretSdkCredentials("test");

            try
            {
                credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeAssignableTo(typeof(CheckoutAuthorizationException));
                ex.Message.ShouldBe("Operation requires OAuth authorization type");
            }
        }
    }
}