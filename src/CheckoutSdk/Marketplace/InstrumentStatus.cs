using System.Runtime.Serialization;

namespace Checkout.Marketplace
{
    public enum InstrumentStatus
    {
        [EnumMember(Value = "verified")]
        Verified,

        [EnumMember(Value = "unverified")]
        Unverified,

        [EnumMember(Value = "pending")]
        Pending
    }
}