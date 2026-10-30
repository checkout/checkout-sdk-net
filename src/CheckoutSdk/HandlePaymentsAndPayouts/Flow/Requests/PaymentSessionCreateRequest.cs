using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Request to create a payment session.
    /// </summary>
    public class PaymentSessionCreateRequest : PaymentSessionCreateBase
    {
        /// <summary>
        /// A timestamp specifying when the PaymentSession should expire, as an ISO 8601 code. If no value is
        /// provided, expiry is set to 24 hours after the PaymentSession is created. You cannot set the session
        /// expiry to more than 60 days after the PaymentSession is created.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Specifies which payment method options to present to the customer. The values in this field override
        /// any equivalent values in disabled_payment_methods.
        /// [Optional]
        /// </summary>
        public IList<PaymentMethod> EnabledPaymentMethods { get; set; }

        /// <summary>
        /// Specifies which payment method options to not present to the customer. If the same payment method is
        /// also in enabled_payment_methods, this value is overridden.
        /// [Optional]
        /// </summary>
        public IList<PaymentMethod> DisabledPaymentMethods { get; set; }

        /// <summary>
        /// Configurations for payment method-specific settings.
        /// [Optional]
        /// </summary>
        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }

        /// <summary>
        /// Configures the parameters for customer retries (payment attempts performed by the customer).
        /// [Optional]
        /// </summary>
        public CustomerRetry CustomerRetry { get; set; }

        /// <summary>
        /// The Customer's IP address. Only IPv4 and IPv6 addresses are accepted.
        /// [Optional]
        /// Deprecated
        /// </summary>
        [Obsolete("This property is deprecated in the API.", false)]
        public string IpAddress { get; set; }
    }
}
