namespace Checkout.Common
{
    /// <summary>
    /// Mobile deep-link redirect URLs returned in 202 payment response _links.redirect.
    /// </summary>
    public class MobileRedirectLink
    {
        /// <summary>
        /// The Android deep-link redirect URL.
        /// [Optional]
        /// </summary>
        public PlatformLink Android { get; set; }

        /// <summary>
        /// The iOS deep-link redirect URL.
        /// [Optional]
        /// </summary>
        public PlatformLink Ios { get; set; }
    }
}
