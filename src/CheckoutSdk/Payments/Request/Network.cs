namespace Checkout.Payments.Request
{
    public class Network
    {
        /// <summary>
        /// The device's IPv4 address. Not required if you provide the Ipv6 field.
        /// [Optional]
        /// </summary>
        public string Ipv4 { get; set; }

        /// <summary>
        /// The device's IPv6 address. Not required if you provide the Ipv4 field.
        /// [Optional]
        /// </summary>
        public string Ipv6 { get; set; }

        /// <summary>
        /// Specifies if the Tor network was used in the browser session.
        /// [Optional]
        /// </summary>
        public bool? Tor { get; set; }

        /// <summary>
        /// Specifies if a virtual private network (VPN) was used in the browser session.
        /// [Optional]
        /// </summary>
        public bool? Vpn { get; set; }

        /// <summary>
        /// Specifies if a proxy was used in the browser session.
        /// [Optional]
        /// </summary>
        public bool? Proxy { get; set; }
    }
}