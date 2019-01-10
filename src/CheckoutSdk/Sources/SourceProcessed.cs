using Newtonsoft.Json;
using Checkout.Common;
using System.Collections.Generic;

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
        [JsonProperty(PropertyName = "response_code")]
        public string ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the customer of the source.
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResponseData"/> of the source.
        /// </summary>
        [JsonProperty(PropertyName = "response_data")]
        public ResponseData ResponseData { get; set; }

        /// <summary>
        /// Gets or sets the links of the source.
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

    }
}