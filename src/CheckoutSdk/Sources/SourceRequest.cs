using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines a request for a source.
    /// </summary>
    public class SourceRequest
    {
        /// <summary>
        /// Creates a new default source request.
        /// </summary>
        public SourceRequest() { }

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the reference of the source.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the billing address of the source.
        /// </summary>
        [JsonProperty(PropertyName = "billing_address")]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the source.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Gets or sets the customer of the source.
        /// </summary>
        public CustomerRequest Customer { get; set; }

        /// <summary>
        /// Gets or sets the specific source data of the source.
        /// </summary>
        [JsonProperty(PropertyName = "source_data")]
        public SourceData SourceData { get; set; }
    }
}
