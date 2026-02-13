using System.Runtime.Serialization;

namespace Checkout.Issuing.ControlGroups
{
    public enum FailIfType
    {
        [EnumMember(Value = "all_fail")] AllFail,
        [EnumMember(Value = "any_fail")] AnyFail
    }
}