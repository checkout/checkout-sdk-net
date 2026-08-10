namespace Checkout
{
    /// <summary>
    /// Every client the suite builds has to choose a domain now that the merchant-specific
    /// subdomain is mandatory, so they all come through here.
    ///
    /// The suite uses the shared hosts. It would be better to exercise the merchant-specific
    /// subdomain, since that is the path merchants are being moved to, but the sandbox OAuth
    /// clients are not provisioned for it: pointing the token request at
    /// {subdomain}.access.sandbox.checkout.com returns invalid_client for every integration
    /// test. Until those clients are bound to the subdomain, CI has to use the legacy hosts.
    /// </summary>
    public static class TestDomainConfiguration
    {
        public static AbstractCheckoutSdkBuilder<T> ConfigureDomain<T>(this AbstractCheckoutSdkBuilder<T> builder)
            where T : ICheckoutApiClient
        {
#pragma warning disable CS0618 // see the class remarks: the sandbox OAuth clients have no subdomain
            return builder.UseLegacyDomain();
#pragma warning restore CS0618
        }
    }
}
