using System.Text.RegularExpressions;

namespace Checkout
{
    public class DefaultStaticKeysSdkCredentials : AbstractStaticKeysSdkCredentials
    {
        private const string DefaultSecretKeyPattern = "^sk_(test_)?(\\w{8})-(\\w{4})-(\\w{4})-(\\w{4})-(\\w{12})$";
        private const string DefaultPublicKeyPatten = "^pk_(test_)?(\\w{8})-(\\w{4})-(\\w{4})-(\\w{4})-(\\w{12})$";

        public DefaultStaticKeysSdkCredentials(string secretKey, string publicKey)
            : base(PlatformType.Default,
                new Regex(DefaultSecretKeyPattern),
                new Regex(DefaultPublicKeyPatten),
                secretKey, publicKey)
        {
        }

        public override SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType)
        {
            switch (authorizationType)
            {
                case SdkAuthorizationType.SecretKey:
                case SdkAuthorizationType.SecretKeyOrOAuth:
                    return new SdkAuthorization(PlatformType, SecretKey);
                case SdkAuthorizationType.PublicKey:
                case SdkAuthorizationType.PublicKeyOrOAuth:
                    return new SdkAuthorization(PlatformType, PublicKey);
                default:
                    throw CheckoutAuthorizationException.InvalidAuthorization(authorizationType);
            }
        }
    }
}