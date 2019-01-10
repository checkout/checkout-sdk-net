using Checkout.Common;

namespace Checkout.Sources
{
    /// <summary>
    /// Indicates the source is pending, either for deferred processing or awaiting redirect.
    /// </summary>
    public class SourcePending : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the source.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Determines whether the source requires a redirect.
        /// </summary>
        /// <returns>True if a redirect is required, otherwise False.</returns>
        public bool RequiresRedirect() => HasLink("redirect");
        
        /// <summary>
        /// Gets the redirect link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetRedirectLink() => GetLink("redirect");
    }
}