using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class Headers : IHeaders
    {
        /// <summary>
        /// The <c>If-Match</c> HTTP header. Carries the ETag of the resource for optimistic
        /// concurrency control when updating a sub-entity.
        /// </summary>
        [JsonProperty(PropertyName = "if-match")]
        public string IfMatch { get; set; }

        /// <summary>
        /// The <c>Accept</c> HTTP header. Used to request a specific Accounts API schema version,
        /// for example <c>application/json;schema_version=3.0</c>.
        /// </summary>
        [JsonProperty(PropertyName = "Accept")]
        public string Accept { get; set; }
    }
}