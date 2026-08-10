namespace Checkout
{
    /// <summary>
    /// The merchant-specific subdomain is mandatory, so the test suite reads it from
    /// CHECKOUT_MERCHANT_SUBDOMAIN. Where that variable is not configured the suite falls back
    /// to the shared hosts, which is the only reason anything here touches the deprecated
    /// opt-out.
    /// </summary>
    public static class TestDomainConfiguration
    {
        public static AbstractCheckoutSdkBuilder<T> ConfigureDomain<T>(this AbstractCheckoutSdkBuilder<T> builder)
            where T : ICheckoutApiClient
        {
            var subdomain = System.Environment.GetEnvironmentVariable("CHECKOUT_MERCHANT_SUBDOMAIN");
            if (!string.IsNullOrWhiteSpace(subdomain))
            {
                return builder.EnvironmentSubdomain(subdomain);
            }

#pragma warning disable CS0618 // no subdomain configured for the suite, fall back to the shared hosts
            return builder.UseLegacyDomain();
#pragma warning restore CS0618
        }
    }
}
