namespace Checkout.Authentication
{
    public class SessionSecretSdkCredentials : SdkCredentials
    {
        public string Secret { get; }

        public SessionSecretSdkCredentials(string secret) : base(PlatformType.Custom)
        {
            CheckoutUtils.ValidateParams("secret", secret);
            Secret = secret;
        }

        public override SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType)
        {
            if (SdkAuthorizationType.Custom.Equals(authorizationType))
            {
                return new SdkAuthorization(PlatformType, Secret);
            }

            throw CheckoutAuthorizationException.InvalidAuthorization(authorizationType);
        }
    }
}