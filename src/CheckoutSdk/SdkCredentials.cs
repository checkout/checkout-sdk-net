namespace Checkout
{
    public abstract class SdkCredentials
    {
        public PlatformType PlatformType { get; }

        protected SdkCredentials(PlatformType platformType)
        {
            PlatformType = platformType;
        }

        public abstract SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType);
    }
}