﻿using System.Runtime.Serialization;

namespace Checkout.Workflows.Actions
{
    public enum WorkflowActionStatus
    {
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "successful")] Successful,
        [EnumMember(Value = "failed")] Failed
    }
}