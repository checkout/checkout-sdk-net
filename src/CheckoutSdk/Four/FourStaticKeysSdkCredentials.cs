using System.Text.RegularExpressions;

namespace Checkout.Four
{
    public class FourStaticKeysSdkCredentials : AbstractStaticKeysSdkCredentials
    {
        private const string FourSecretKeyPattern = "^sk_(sbox_)?[a-z2-7]{26}[a-z2-7*#$=]$";
        private const string FourPublicKeyPatten = "^pk_(sbox_)?[a-z2-7]{26}[a-z2-7*#$=]$";

        public FourStaticKeysSdkCredentials(string secretKey, string publicKey)
            : base(PlatformType.Four,
                new Regex(FourSecretKeyPattern),
                new Regex(FourPublicKeyPatten),
                secretKey, publicKey)
        {
        }

        public override SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType)
        {
            switch (authorizationType)
            {
                case SdkAuthorizationType.SecretKey:
                    return new SdkAuthorization(PlatformType, SecretKey);

                case SdkAuthorizationType.PublicKey:
                    return new SdkAuthorization(PlatformType, PublicKey);

                default:
                    throw CheckoutAuthorizationException.InvalidAuthorization(authorizationType);
            }
        }
    }
}