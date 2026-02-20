using System.Collections.Generic;
using Checkout.Issuing.Common.Responses;
using Checkout.Issuing.ControlGroups.Common;

namespace Checkout.Issuing.ControlGroups.Responses
{
    /// <summary>
    /// Control group response containing the details of a control group.
    /// </summary>
    public class ControlGroupResponse : ControlIssuingResponseBase
    {
        /// <summary>
        /// The ID of the card or control profile.
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
        /// Indicates whether the control group is set to readonly. Control groups marked as non-editable are predefined by Checkout and are not modifiable by clients.
        /// [Required]
        /// </summary>
        public bool? IsEditable { get; set; }

        /// <summary>
        /// A description for the control group.
        /// &lt;= 256 characters
        /// [Optional]
        /// </summary>
        public string Description { get; set; }
    }
}