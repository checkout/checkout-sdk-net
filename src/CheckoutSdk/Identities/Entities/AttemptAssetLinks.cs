using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The links related to an attempt asset.
    /// </summary>
    public class AttemptAssetLinks
    {
        /// <summary>
        /// The URL to download the asset.
        /// [Required]
        /// Format: uri
        /// </summary>
        [JsonProperty(PropertyName = "asset_url")]
        public Link AssetUrl { get; set; }
    }
}
