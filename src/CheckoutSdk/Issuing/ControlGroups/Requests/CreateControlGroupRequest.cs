using System.Collections.Generic;

namespace Checkout.Issuing.ControlGroups.Requests
{
    public class CreateControlGroupRequest
    {
        public string TargetId { get; set; }

        public FailIfType? FailIf { get; set; }

        public IList<ControlGroupControl> Controls { get; set; }

        public string Description { get; set; }
    }
}