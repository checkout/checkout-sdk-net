namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The device on which the digital card was provisioned.
    /// [Optional]
    /// </summary>
    public class IssuingDigitalCardDevice
    {
        /// <summary>
        /// The device identifier.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The device type.
        /// [Optional]
        /// </summary>
        public IssuingDigitalCardDeviceType? Type { get; set; }

        /// <summary>
        /// The device manufacturer.
        /// [Optional]
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// The device brand.
        /// [Optional]
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The device model.
        /// [Optional]
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The device operating system version.
        /// [Optional]
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// The device firmware version.
        /// [Optional]
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// The device phone number.
        /// [Optional]
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The device name.
        /// [Optional]
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// The device parent identifier.
        /// [Optional]
        /// </summary>
        public string DeviceParentId { get; set; }

        /// <summary>
        /// The device language.
        /// [Optional]
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The device serial number.
        /// [Optional]
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// The device time zone.
        /// [Optional]
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// The device time zone setting.
        /// [Optional]
        /// </summary>
        public IssuingDigitalCardDeviceTimeZoneSetting? TimeZoneSetting { get; set; }

        /// <summary>
        /// The device SIM serial number.
        /// [Optional]
        /// </summary>
        public string SimSerialNumber { get; set; }

        /// <summary>
        /// The device International Mobile Equipment Identity (IMEI) number.
        /// [Optional]
        /// </summary>
        public string Imei { get; set; }

        /// <summary>
        /// The device network operator.
        /// [Optional]
        /// </summary>
        public string NetworkOperator { get; set; }

        /// <summary>
        /// The device network type.
        /// [Optional]
        /// </summary>
        public IssuingDigitalCardDeviceNetworkType? NetworkType { get; set; }
    }
}
