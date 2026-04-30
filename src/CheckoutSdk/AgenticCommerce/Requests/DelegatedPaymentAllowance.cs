using Checkout.AgenticCommerce.Entities;
using Checkout.Common;
using System;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// The spending constraints that define what the delegated payment token is authorized for.
    /// </summary>
    public class DelegatedPaymentAllowance
    {
        /// <summary>
        /// The reason for the allowance.
        /// [Required]
        /// </summary>
        public DelegatedPaymentAllowanceReason Reason { get; set; }

        /// <summary>
        /// The maximum amount that can be charged using the delegated payment token,
        /// in the minor currency unit.
        /// [Required]
        /// </summary>
        public long MaxAmount { get; set; }

        /// <summary>
        /// The three-letter ISO 4217 currency code.
        /// [Required]
        /// 3 characters.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The unique identifier of the merchant that will process the payment.
        /// [Required]
        /// &lt;= 256 characters.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// The identifier of the checkout session associated with this delegated payment.
        /// [Required]
        /// </summary>
        public string CheckoutSessionId { get; set; }

        /// <summary>
        /// The expiry time of the delegated payment token, in RFC 3339 format.
        /// Must be a time in the future.
        /// [Required]
        /// Format: date-time.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
