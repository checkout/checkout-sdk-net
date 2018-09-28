namespace Checkout.Common
{
    /// <summary>
    /// Hypermedia link as per the JSON HAL specification (http://stateless.co/hal_specification.html).
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets the URI of the link.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets a title describing the links.
        /// </summary>
        public string Title { get; set; }
    }
}