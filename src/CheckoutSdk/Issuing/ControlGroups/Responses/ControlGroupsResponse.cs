using System.Collections.Generic;

namespace Checkout.Issuing.ControlGroups.Responses
{
    /// <summary>
    /// Response containing a list of control groups applied to the specified target.
    /// </summary>
    public class ControlGroupsResponse : HttpMetadata
    {
        /// <summary>
        /// The list of control groups applied to the specified target.
        /// [Required]
        /// </summary>
        public IList<ControlGroupResponse> ControlGroups { get; set; }
    }
}