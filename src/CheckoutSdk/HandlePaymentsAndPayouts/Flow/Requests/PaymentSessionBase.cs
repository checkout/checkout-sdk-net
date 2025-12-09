using Checkout.Payments;

using Product = Checkout.Payments.Request.Product;

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Base class for all payment session requests containing common properties
    /// </summary>
    public abstract class PaymentSessionBase
    {
        /// <summary>
        /// The payment amount. Provide a value of 0 to perform a card verification.
        /// The amount must be provided in the minor currency unit.
        /// For example, provide 10000 for £100.00, or provide 100 for ¥100 (a zero-decimal currency).
        /// [Required]
        /// </summary>
        public long? Amount { get; set; }
        
        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number.
        /// For Amex payments, this must be at most 30 characters.
        /// For Benefit payments, the reference must be a unique alphanumeric value.
        /// For iDEAL payments, the reference is required and must be an alphanumeric value with a 35-character limit.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The line items in the order.
        /// </summary>
        public IList<Product> Items { get; set; }

        /// <summary>
        /// Information required for 3D Secure authentication payments.
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsRequest ThreeDS { get; set; }

        /// <summary>
        /// Must be specified for card-not-present (CNP) payments. Default: "Regular"
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Checkout.Payments.PaymentType.Regular;
    }
}