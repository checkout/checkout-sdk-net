using System;
using System.Net.Http.Headers;
using Checkout.Four;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class FourStaticKeysSdkCredentialsTest : UnitTestFixture
    {
        private const string Bearer = "Bearer";

        [Fact]
        private void ShouldFailCreatingFourStaticKeysSdkCredentials_invalidKeys()
        {
            try
            {
                new FourStaticKeysSdkCredentials(ValidFourSk, InvalidFourPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new FourStaticKeysSdkCredentials(InvalidFourSk, ValidFourPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid secret key");
            }
        }

        [Fact]
        private void Four_shouldCreateFourStaticKeysSdkCredentials()
        {
            var credentials = new FourStaticKeysSdkCredentials(ValidFourSk, ValidFourPk);
            credentials.PlatformType.ShouldBe(PlatformType.Four);
            credentials.PublicKey.ShouldBe(ValidFourPk);
            credentials.SecretKey.ShouldBe(ValidFourSk);
        }

        [Fact]
        private void ShouldCreateFourStaticKeysSdkCredentialsForProd()
        {
            const string validFourSk = "sk_m73dzbpy7cf3gfd46xr4yj5xo4e";
            const string validFourPk = "pk_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            var credentials = new FourStaticKeysSdkCredentials(validFourSk, validFourPk);

            credentials.PlatformType.ShouldBe(PlatformType.Four);
            credentials.PublicKey.ShouldBe(validFourPk);
            credentials.SecretKey.ShouldBe(validFourSk);
        }

        [Fact]
        private void ShouldFailToCreateFourStaticKeysSdkCredentialsForProd()
        {
            const string similarFourSk = "sk_tost_m73dzbpy7cf3gfd46xr4yj5xo4e";
            const string similarFourPk = "pk_tost_pkhpdtvmkgf7hdnpwnbhw7r2uic";

            try
            {
                new FourStaticKeysSdkCredentials(ValidFourSk, similarFourPk);
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                e.Message.ShouldBe("invalid public key");
            }

            try
            {
                new FourStaticKeysSdkCredentials(similarFourSk, ValidFourSk);
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
            var credentials = new FourStaticKeysSdkCredentials(ValidFourSk, ValidFourPk);

            var auth1 = credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey);
            auth1.ShouldNotBeNull();
            auth1.GetAuthorizationHeader().ShouldBe(new AuthenticationHeaderValue(Bearer, ValidFourSk));

            var auth2 = credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey);
            auth2.ShouldNotBeNull();
            auth2.GetAuthorizationHeader().ShouldBe(new AuthenticationHeaderValue(Bearer, ValidFourPk));
        }
    }
}