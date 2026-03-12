using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class GooglePayConfiguration : PaymentMethodConfigurationBase
    {
        /// <summary>
        /// The status of the Google Pay payment total price. Default: "final"
        /// </summary>
        public TotalPriceStatus? TotalPriceStatus { get; set; } = Entities.TotalPriceStatus.Final;
    }
}