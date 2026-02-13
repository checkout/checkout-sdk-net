using System.Collections.Generic;

namespace Checkout.Issuing.ControlGroups.Responses
{
    public class ControlGroupsResponse : HttpMetadata
    {
        public IList<ControlGroupResponse> ControlGroups { get; set; }
    }
}