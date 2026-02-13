using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Issuing.ControlGroups.Responses
{
    public class ControlGroupResponse : Resource
    {
        public string Id { get; set; }

        public string TargetId { get; set; }

        public FailIfType? FailIf { get; set; }

        public IList<ControlGroupControl> Controls { get; set; }

        public bool? IsEditable { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string Description { get; set; }
    }
}