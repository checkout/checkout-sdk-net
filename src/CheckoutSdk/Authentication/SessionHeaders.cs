using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests;
using Newtonsoft.Json;

namespace Checkout.Authentication
{
    /// <summary>
    /// Custom headers for the GET /sessions/{id} endpoint.
    /// </summary>
    public class SessionHeaders : IHeaders
    {
        /// <summary>
        /// Optionally provide the type of channel so you only get the relevant actions.
        /// If not provided and the status is "pending", next_actions will return
        /// "collect_channel_data" and, if available, "issuer_fingerprint".
        /// </summary>
        [JsonProperty(PropertyName = "channel")]
        public ChannelType? Channel { get; set; }
    }
}
