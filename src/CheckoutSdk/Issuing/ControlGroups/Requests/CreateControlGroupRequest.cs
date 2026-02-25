using System.Collections.Generic;
using Checkout.Issuing.ControlGroups.Common;

namespace Checkout.Issuing.ControlGroups.Requests
{
    /// <summary>
    /// Creates a control group and applies it to the specified target.
    /// </summary>
    public class CreateControlGroupRequest
    {
        /// <summary>
        /// The ID of the card or control profile to apply the control to. Note that control profiles cannot be a target for velocity_limit controls.
        /// ^(crd|cpr)_[a-z0-9]{26}$
        /// 30 characters
        /// [Required]
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Sets how to determine the result of the group.
        /// Enum: "all_fail" "any_fail"
        /// [Required]
        /// </summary>
        public FailIfType? FailIf { get; set; }

        /// <summary>
        /// The controls that belong to the group.
        /// [Required]
        /// </summary>
        public IList<ControlGroupControl> Controls { get; set; }

        /// <summary>
        /// A description for the control group.
        /// &lt;= 256 characters
        /// [Optional]
        /// </summary>
        public string Description { get; set; }
    }
}