using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Terminal details.
    /// </summary>
    public class PaymentSetupTerminal
    {
        /// <summary>
        /// Terminal identifier.
        /// [Optional]
        /// min 8 characters, max 8 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The local date and time on the terminal, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LocalDateTime { get; set; }
    }
}
