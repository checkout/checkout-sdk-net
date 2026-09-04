using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The status of the network token attached to a stored card instrument.
    /// </summary>
    public enum InstrumentNetworkTokenState
    {
        [EnumMember(Value = "active")]
        Active,

        [EnumMember(Value = "suspended")]
        Suspended,

        [EnumMember(Value = "inactive")]
        Inactive,
    }
}
