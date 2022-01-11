namespace Checkout
{
    public abstract class AbstractClient
    {
        protected readonly IApiClient ApiClient;
        private readonly CheckoutConfiguration _configuration;
        private readonly SdkAuthorizationType _sdkAuthorizationType;

        protected AbstractClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration,
            SdkAuthorizationType sdkAuthorizationType)
        {
            ApiClient = apiClient;
            _configuration = configuration;
            _sdkAuthorizationType = sdkAuthorizationType;
        }


        protected SdkAuthorization SdkAuthorization()
        {
            return _configuration.SdkCredentials.GetSdkAuthorization(_sdkAuthorizationType);
        }

        protected SdkAuthorization SdkAuthorization(SdkAuthorizationType sdkAuthorizationType)
        {
            return _configuration.SdkCredentials.GetSdkAuthorization(sdkAuthorizationType);
        }

        protected static string BuildPath(params string[] pathParams)
        {
            return string.Join("/", pathParams);
        }

        protected bool IsSandbox()
        {
            return _configuration.Environment.Equals(Environment.Sandbox);
        }
    }
}