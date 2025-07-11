using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.Common.Responses
{
    public enum StateType
    {
        [EnumMember(Value = "active")]
        Active,
        
        [EnumMember(Value = "suspended")]
        Suspended,
        
        [EnumMember(Value = "inactive")]
        Inactive,
        
        [EnumMember(Value = "declined")]
        Declined,
        
        [EnumMember(Value = "requested")]
        Requested
    }
}
