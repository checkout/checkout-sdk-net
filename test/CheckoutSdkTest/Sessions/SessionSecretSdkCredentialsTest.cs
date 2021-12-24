using Shouldly;
using System;
using Xunit;

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
            }
            catch (Exception ex)
            {
                ex.ShouldBeAssignableTo(typeof(CheckoutAuthorizationException));
                ex.Message.ShouldBe("Operation does not support OAuth authorization type");
            }
        }
    }
}