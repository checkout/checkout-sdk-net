namespace Checkout.Payments.Request
{
    public class Device
    {
        /// <summary>
        /// The contents of the HTTP User-Agent request header.
        /// Required to process the device with the risk engine.
        /// [Optional]
        /// max 2048 characters
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Details of the device network. Either Ipv4 or Ipv6 is required.
        /// [Optional]
        /// </summary>
        public Network Network { get; set; }

        /// <summary>
        /// Details of the device ID provider.
        /// [Optional]
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        /// The UTC date and time the payment was performed as reported by the device.
        /// Required to process the device with the risk engine.
        /// [Optional]
        /// Format: ISO 8601
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// The time difference between UTC time and the local time reported by the browser, in minutes.
        /// Required to process the device with the risk engine.
        /// [Optional]
        /// min 1 character
        /// max 5 characters
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Specifies if the device is running in a virtual machine.
        /// [Optional]
        /// </summary>
        public bool? VirtualMachine { get; set; }

        /// <summary>
        /// Specifies if the browser is in incognito mode.
        /// [Optional]
        /// </summary>
        public bool? Incognito { get; set; }

        /// <summary>
        /// Specifies if the device is jailbroken.
        /// [Optional]
        /// </summary>
        public bool? Jailbroken { get; set; }

        /// <summary>
        /// Specifies if the device is rooted.
        /// [Optional]
        /// </summary>
        public bool? Rooted { get; set; }

        /// <summary>
        /// Specifies if the browser has the ability to execute Java, as reported by the browser's navigator.javaEnabled property.
        /// [Optional]
        /// </summary>
        public bool? JavaEnabled { get; set; }

        /// <summary>
        /// Specifies if the browser has the ability to execute Javascript, as reported by the browser's navigator.javascriptEnabled property.
        /// Only required for 3D Secure authentications processed with 3DS 2.2. If processed with an older 3DS version, this field is ignored.
        /// [Optional]
        /// </summary>
        public bool? JavascriptEnabled { get; set; }

        /// <summary>
        /// The browser language, as reported by the browser's navigator.language property.
        /// [Optional]
        /// Format: IETF BCP47 language tag
        /// min 1 character
        /// max 12 characters
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The bit depth of the color palette for displaying images in bits per pixel, as reported by the browser's screen.colorDepth property.
        /// [Optional]
        /// min 1 character
        /// max 2 characters
        /// </summary>
        public string ColorDepth { get; set; }

        /// <summary>
        /// The total height of the device screen in pixels, as reported by the browser's screen.height property.
        /// [Optional]
        /// min 1 character
        /// max 6 characters
        /// </summary>
        public string ScreenHeight { get; set; }

        /// <summary>
        /// The total width of the device screen in pixels, as reported by the browser's screen.width property.
        /// [Optional]
        /// min 1 character
        /// max 6 characters
        /// </summary>
        public string ScreenWidth { get; set; }
    }
}