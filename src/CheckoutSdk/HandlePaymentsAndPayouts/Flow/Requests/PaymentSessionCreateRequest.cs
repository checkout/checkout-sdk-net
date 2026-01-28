using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.Payments.Request;

using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Request to create a payment session.
    /// </summary>
    public class PaymentSessionCreateRequest : PaymentSessionInfo
    {
        /// <summary>
        /// A timestamp specifying when the PaymentSession should expire, as an ISO 8601 code.
        /// </summary>
        public DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Specifies which payment method options to present to the customer.
        /// </summary>
        public IList<PaymentMethod> EnabledPaymentMethods { get; set; }

        /// <summary>
        /// Specifies which payment method options to not present to the customer.
        /// </summary>
        public IList<PaymentMethod> DisabledPaymentMethods { get; set; }

        /// <summary>
        /// Configurations for payment method-specific settings.
        /// </summary>
        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }

        /// <summary>
        /// Configuration for asynchronous retries.
        /// </summary>
        public PaymentRetryRequest CustomerRetry { get; set; }

        /// <summary>
        /// Deprecated - The Customer's IP address. Only IPv4 and IPv6 addresses are accepted.
        /// </summary>
        public string IpAddress { get; set; }
    }
}