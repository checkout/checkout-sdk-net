using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class CustomerRetry
    {
        /// <summary>
        /// The maximum number of authorization retry attempts, excluding the initial authorization. Default: 5
        /// </summary>
        public int? MaxAttempts { get; set; } = 5;
    }
}