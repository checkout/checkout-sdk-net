using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    public enum IssuingControlType
    {
        [EnumMember(Value = "velocity_limit")] VelocityLimit,
        [EnumMember(Value = "mcc_limit")] MccLimit,
        [EnumMember(Value = "mid_limit")] MidLimit
    }
}