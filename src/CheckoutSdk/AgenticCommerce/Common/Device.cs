namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// The user's device
    /// </summary>
    public class Device
    {
        /// <summary>
        /// The device's IP address
        /// [Required]
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The device's user agent
        /// [Required]
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The device brand
        /// [Optional]
        /// </summary>
        public string DeviceBrand { get; set; }

        /// <summary>
        /// The device type
        /// [Optional]
        /// </summary>
        public string DeviceType { get; set; }
    }
}