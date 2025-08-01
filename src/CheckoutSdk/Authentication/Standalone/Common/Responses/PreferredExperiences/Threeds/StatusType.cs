using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences.Threeds
{
    public enum StatusType
    {
        [EnumMember(Value = "available")]
        Available,

        [EnumMember(Value = "unprocessed")]
        Unprocessed,

        [EnumMember(Value = "processed")]
        Processed,

        [EnumMember(Value = "unavailable")]
        Unavailable,
    }
}