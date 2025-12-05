using Checkout.Common;

using System;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Recipient
    {
        /// <summary>
        /// The recipient's date of birth, in the format YYYY-MM-DD.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// An identifier related to the primary recipient's account. 
        /// For example, an IBAN, an internal account number, a phone number, 
        /// or the first six and last four digits of the PAN.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The recipient's address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The recipient's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The recipient's last name.
        /// </summary>
        public string LastName { get; set; }
    }
}