using System.Runtime.Serialization;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The device network type.
    /// Enum: "googlepay" "applepay" "remote_commerce_programs"
    /// </summary>
    public enum IssuingDigitalCardDeviceNetworkType
    {
        [EnumMember(Value = "cellular")]
        Cellular,
        [EnumMember(Value = "wifi")]
        Wifi,
    }
}
