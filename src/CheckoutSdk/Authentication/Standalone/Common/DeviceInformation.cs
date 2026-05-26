namespace Checkout.Authentication.Standalone.Common
{
    /// <summary>
    /// Details of the device from which the authentication originated.
    /// </summary>
    public class DeviceInformation
    {
        /// <summary>
        /// The unique identifier for the device.
        /// [Optional]
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Device session ID collected from our standalone Risk.js package.
        /// [Optional]
        /// Pattern: ^(dsid)_(\w{26})$
        /// </summary>
        public string DeviceSessionId { get; set; }
    }
}
