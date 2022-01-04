﻿namespace Checkout
{
    public abstract class AbstractClient
    {
        protected readonly IApiClient ApiClient;
        protected readonly ICheckoutConfiguration _configuration;
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

        protected AbstractClient(
            IApiClient apiClient,
            ICheckoutConfiguration configuration,
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