using System;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Account funding transaction recipient details.
    /// </summary>
    public class AccountFundingTransactionRecipient
    {
        /// <summary>
        /// Date of birth of the recipient.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Any identifier like part of the PAN (first six digits and last four digits), an IBAN,
        /// an internal account number, or a phone number related to the primary recipient's account.
        /// [Optional]
        /// &lt;= 34 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The recipient's first name.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The recipient's last name.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The recipient's address.
        /// [Optional]
        /// </summary>
        public Address Address { get; set; }
    }
}
