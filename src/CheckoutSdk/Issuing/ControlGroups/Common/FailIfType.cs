using System.Runtime.Serialization;

namespace Checkout.Issuing.ControlGroups.Common
{
    /// <summary>
    /// Defines how to determine the result of the control group.
    /// </summary>
    public enum FailIfType
    {
        /// <summary>
        /// The control group fails only if all controls in the group fail.
        /// </summary>
        [EnumMember(Value = "all_fail")] AllFail,
        
        /// <summary>
        /// The control group fails if any control in the group fails.
        /// </summary>
        [EnumMember(Value = "any_fail")] AnyFail
    }
}