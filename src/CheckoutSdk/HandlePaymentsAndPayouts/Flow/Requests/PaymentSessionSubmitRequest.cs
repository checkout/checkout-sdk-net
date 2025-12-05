
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.Payments.Request;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    public class PaymentSessionSubmitRequest
    {
        /// <summary>
        /// A unique token representing the additional customer data captured by Flow, 
        /// as received from the handleSubmit callback.
        /// Do not log or store this value.
        /// </summary>
        public string SessionData { get; set; }

        /// <summary>
        /// The payment amount. Provide a value of 0 to perform a card verification.
        /// The amount must be provided in the minor currency unit.
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The line items in the order.
        /// </summary>
        public IList<Checkout.Payments.Request.Product> Items { get; set; }

        /// <summary>
        /// Information required for 3D Secure authentication payments.
        /// </summary>
        public ThreeDSRequest ThreeDS { get; set; }

        /// <summary>
        /// Deprecated - The Customer's IP address. Only IPv4 and IPv6 addresses are accepted.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Must be specified for card-not-present (CNP) payments. Default: "Regular"
        /// </summary>
        public Checkout.Payments.PaymentType? PaymentType { get; set; } = Checkout.Payments.PaymentType.Regular;
    }
}