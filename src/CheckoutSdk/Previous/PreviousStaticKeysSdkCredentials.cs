using System.Text.RegularExpressions;

namespace Checkout.Previous
{
    public class PreviousStaticKeysSdkCredentials : AbstractStaticKeysSdkCredentials
    {
        private const string PreviousSecretKeyPattern = "^sk_(test_)?(\\w{8})-(\\w{4})-(\\w{4})-(\\w{4})-(\\w{12})$";
        private const string PreviousPublicKeyPatten = "^pk_(test_)?(\\w{8})-(\\w{4})-(\\w{4})-(\\w{4})-(\\w{12})$";

        public PreviousStaticKeysSdkCredentials(string secretKey, string publicKey)
            : base(PlatformType.Previous,
                new Regex(PreviousSecretKeyPattern),
                new Regex(PreviousPublicKeyPatten),
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