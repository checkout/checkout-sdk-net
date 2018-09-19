using Newtonsoft.Json;
using System;

namespace Checkout.Payments
{
    public class PaymentRecipient
    {
        /// <summary>
        /// Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions. 
        /// </summary>
        /// <param name="dob">The recipient's date of birth</param>
        /// <param name="accountNumber">The first six digits and the last four digits of the primary recipient's card, without spaces, or, up to ten characters of the primary recipient's account number</param>
        /// <param name="zip">The first part of the UK postcode for example W1T 4TJ would be W1T</param>
        /// <param name="lastName">The last name of the recipient</param>
        public PaymentRecipient(DateTime dob, string accountNumber, string zip, string lastName)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length > 10)
                throw new ArgumentException(
                    $"{nameof(accountNumber)} cannot be null or whitespace and its length cannot exceed 10.");

            if (string.IsNullOrWhiteSpace(zip))
                throw new ArgumentException($"{nameof(zip)} cannot be null or whitespace.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException($"{nameof(lastName)} cannot be null or whitespace.");

            Dob = dob;
            AccountNumber = accountNumber;
            Zip = zip;
            LastName = lastName;
        }

        /// <summary>
        /// The recipient's date of birth
        /// </summary>
        [JsonConverter(typeof(DateTimeFormatConverter), "yyyy-MM-dd")]
        public DateTime Dob { get; }
        /// <summary>
        /// The first six digits and the last four digits of the primary recipient's card, without spaces, or, up to ten characters of the primary recipient's account number
        /// </summary>
        public string AccountNumber { get; }
        /// <summary>
        /// The first part of the UK postcode for example W1T 4TJ would be W1T
        /// </summary>
        public string Zip { get; }
        /// <summary>
        /// The last name of the recipient
        /// </summary>
        public string LastName { get; }
    }
}