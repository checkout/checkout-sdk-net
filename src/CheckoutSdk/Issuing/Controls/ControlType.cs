using System.Runtime.Serialization;

namespace Checkout.Issuing.Controls
{
    public enum ControlType
    {
        [EnumMember(Value = "velocity_limit")] VelocityLimit,
        [EnumMember(Value = "mcc_limit")] MccLimit
    }
}