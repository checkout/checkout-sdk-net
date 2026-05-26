using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Account funding transaction sender details.
    /// </summary>
    public class AccountFundingTransactionSender
    {
        /// <summary>
        /// Date of birth of the sender.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// The unique reference for the sender of the payment.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Sender identification details.
        /// [Optional]
        /// </summary>
        public AccountFundingTransactionIdentification Identification { get; set; }
    }
}
