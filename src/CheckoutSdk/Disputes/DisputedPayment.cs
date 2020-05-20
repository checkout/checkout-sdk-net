using System;

namespace Checkout.Disputes
{
    /// <summary>
    /// Represents a <see cref="DisputedPayment"/>.
    /// </summary>
    public class DisputedPayment
    {
        /// <summary>
        /// Gets or sets the unique identifier of the disputed payment.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the reference of the disputed payment.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the amount of the disputed payment.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency of the disputed payment.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the payment method used for the disputed payment.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the acquirer reference number (ARN) of for the disputed payment.
        /// </summary>
        public string Arn { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time at which the disputed payment was requested.
        /// </summary>
        public DateTime ProcessedOn { get; set; }
    }
}
