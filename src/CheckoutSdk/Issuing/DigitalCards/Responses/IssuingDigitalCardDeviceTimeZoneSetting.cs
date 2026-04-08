using System.Runtime.Serialization;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The device time zone setting type.
    /// </summary>
    public enum IssuingDigitalCardDeviceTimeZoneSetting
    {
        [EnumMember(Value = "network_set")] NetworkSet,
        [EnumMember(Value = "consumer_set")] ConsumerSet,
    }
}
