using Shouldly;
using System;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class StaticKeysSdkCredentialsTest : UnitTestFixture
    {
        private const string Bearer = "Bearer";

        [Fact]
        private void ShouldFailCreatingDefaultStaticKeysSdkCredentials_invalidKeys()
        {
            try
            {
                new StaticKeysSdkCredentials(ValidDefaultSk, InvalidDefaultPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new StaticKeysSdkCredentials(InvalidDefaultSk, ValidDefaultPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        private void Default_shouldCreateDefaultStaticKeysSdkCredentials()
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            credentials.PlatformType.ShouldBe(PlatformType.Default);
            credentials.PublicKey.ShouldBe(ValidDefaultPk);
            credentials.SecretKey.ShouldBe(ValidDefaultSk);
        }

        [Fact]
        private void ShouldCreateDefaultStaticKeysSdkCredentialsForProd()
        {
            const string validDefaultSk = "sk_m73dzbpy7cf3gfd46xr4yj5xo4e";
            const string validDefaultPk = "pk_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            var credentials = new StaticKeysSdkCredentials(validDefaultSk, validDefaultPk);

            credentials.PlatformType.ShouldBe(PlatformType.Default);
            credentials.PublicKey.ShouldBe(validDefaultPk);
            credentials.SecretKey.ShouldBe(validDefaultSk);
        }

        [Fact]
        private void ShouldFailToCreateDefaultStaticKeysSdkCredentialsForProd()
        {
            const string similarDefaultSk = "sk_tost_m73dzbpy7cf3gfd46xr4yj5xo4e";
            const string similarDefaultPk = "pk_tost_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            try
            {
                new StaticKeysSdkCredentials(ValidDefaultSk, similarDefaultPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new StaticKeysSdkCredentials(similarDefaultSk, ValidDefaultSk);
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
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);

            var auth1 = credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey);
            auth1.ShouldNotBeNull();
            auth1.GetAuthorizationHeader().ShouldBe($"{Bearer} {ValidDefaultSk}");

            var auth2 = credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey);
            auth2.ShouldNotBeNull();
            auth2.GetAuthorizationHeader().ShouldBe($"{Bearer} {ValidDefaultPk}");
        }
    }
}