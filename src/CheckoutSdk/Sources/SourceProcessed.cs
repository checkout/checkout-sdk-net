using Checkout.Common;

namespace Checkout.Sources
{
    /// <summary>
    /// Indicates the source has been successfully processed.
    /// </summary>
    public class SourceProcessed : Resource
    {
        /// <summary>
        /// Gets or sets the id of the source.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the response code of the source.
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the customer of the source.
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResponseData"/> of the source.
        /// </summary>
        public ResponseData ResponseData { get; set; }
    }
}
