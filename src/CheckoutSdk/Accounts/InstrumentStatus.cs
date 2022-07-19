using System.Runtime.Serialization;

namespace Checkout.Accounts
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