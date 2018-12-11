using Newtonsoft.Json;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines the response data of a <see cref="SourceResponse"/>.
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Gets or sets the mandate reference of the response data.
        /// </summary>
        [JsonProperty(PropertyName = "mandate_reference")]
        public string MandateReference { get; set; }
    }
}
