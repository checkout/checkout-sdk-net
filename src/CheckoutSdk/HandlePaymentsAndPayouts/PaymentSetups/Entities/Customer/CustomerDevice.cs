namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Details of the customer's device.
    /// </summary>
    public class CustomerDevice
    {
        /// <summary>
        /// The locale of the device, for example en_GB.
        /// [Optional]
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// A unique identifier for the customer's device.
        /// [Optional]
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// The customer's device IPv4 address, used by some payment methods for risk and eligibility
        /// checks.
        /// [Optional]
        /// </summary>
        public string Ipv4 { get; set; }

        /// <summary>
        /// The customer's device IPv6 address, used by some payment methods for risk and eligibility
        /// checks.
        /// [Optional]
        /// </summary>
        public string Ipv6 { get; set; }

        /// <summary>
        /// The type of client the customer uses to initiate the payment. Required when using Cash App
        /// Pay; a Cash App payment setup without it is rejected.
        /// [Optional]
        /// Enum: "web" "mobile_web" "app"
        /// </summary>
        public CustomerDeviceClient? Client { get; set; }

        /// <summary>
        /// The operating system of the customer's device.
        /// [Optional]
        /// Enum: "android" "ios"
        /// </summary>
        public CustomerDeviceOs? Os { get; set; }
    }
}
