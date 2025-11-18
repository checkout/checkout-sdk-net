using Newtonsoft.Json;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Device Information
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// IP address of the device
        /// </summary>
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// User agent string from the browser
        /// </summary>
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        /// <summary>
        /// Device brand (e.g., "apple", "samsung")
        /// </summary>
        [JsonProperty("device_brand")]
        public string DeviceBrand { get; set; }

        /// <summary>
        /// Device type (e.g., "tablet", "mobile", "desktop")
        /// </summary>
        [JsonProperty("device_type")]
        public string DeviceType { get; set; }
    }
}