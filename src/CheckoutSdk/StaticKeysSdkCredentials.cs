using System.Text.RegularExpressions;

namespace Checkout
{
    public class StaticKeysSdkCredentials : AbstractStaticKeysSdkCredentials
    {
         private const string DefaultSecretKeyPattern = "^sk_[0-9a-f]{8}-(?:[0-9a-f]{4}-){3}[0-9a-f]{12}$";
        private const string DefaultPublicKeyPatten = "^pk_[0-9a-f]{8}-(?:[0-9a-f]{4}-){3}[0-9a-f]{12}$";

        public StaticKeysSdkCredentials(string secretKey, string publicKey)
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
