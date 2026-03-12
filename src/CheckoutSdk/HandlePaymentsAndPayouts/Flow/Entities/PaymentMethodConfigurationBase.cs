using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    /// <summary>
    /// Base class for payment method configurations containing common properties
    /// </summary>
    public abstract class PaymentMethodConfigurationBase
    {
        /// <summary>
        /// Specifies whether you intend to store the cardholder's payment details. Default: "disabled"
        /// </summary>
        public StorePaymentDetailsType? StorePaymentDetails { get; set; } = StorePaymentDetailsType.Disabled;

        /// <summary>
        /// The account holder's details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }
    }
}