using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    public enum LimitControlType
    {
        [EnumMember(Value = "allow")] Allow,
        [EnumMember(Value = "block")] Block,
    }
}