using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public enum TotalType
    {
        [EnumMember(Value = "pending")]
        Pending,
        
        [EnumMember(Value = "final")]
        Final
    }
}