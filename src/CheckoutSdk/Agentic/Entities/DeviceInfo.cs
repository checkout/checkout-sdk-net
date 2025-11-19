using Newtonsoft.Json;

namespace Checkout.Agentic.Entities
{
    /// <summary>
    /// Device Information
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// IP address of the device
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User agent string from the browser
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Device brand (e.g., "apple", "samsung")
        /// </summary>
        public string DeviceBrand { get; set; }

        /// <summary>
        /// Device type (e.g., "tablet", "mobile", "desktop")
        /// </summary>
        public string DeviceType { get; set; }
    }
}