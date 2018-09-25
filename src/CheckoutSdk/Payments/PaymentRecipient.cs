using Newtonsoft.Json;
using System;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the recipient of a payment. Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions.
    /// For more information see https://docs.checkout.com/docs/requirements-for-financial-institutions.
    /// </summary>
    public class PaymentRecipient
    {       
        /// <summary>
        /// Creates a new <see cref="PaymentRecipient"/> instance. 
        /// </summary>
        /// <param name="dateOfBirth">The recipient's date of birth</param>
        /// <param name="accountNumber">
        /// If the payment is being made with a VISA card, then provide the first six digits and the last four digits of the primary recipient’s card, without any spaces. Or, the first 10 digits of the primary recipient’s account number.
        /// If the payment is being made with a Mastercard card, then provide the primary recipient's full card number.
        /// </param>
        /// <param name="zip">The first part of the UK postcode for example W1T 4TJ would be W1T.</param>
        /// <param name="lastName">The first six characters of the primary recipient’s surname. Only alphabetic characters are allowed.</param>
        public PaymentRecipient([JsonProperty("dob")]DateTime dateOfBirth, string accountNumber, string zip, string lastName)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length < 10)
                throw new ArgumentException("The recipient's account number must be provided. See https://docs.checkout.com/docs/requirements-for-financial-institutions for scheme specific rules.", nameof(accountNumber));

            if (string.IsNullOrWhiteSpace(zip))
                throw new ArgumentException("The recipient's zip must be provided.", nameof(zip));

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 6)
                throw new ArgumentException("The recipient's last name must be provided and cannot exceed six characters.", nameof(lastName));

            DateOfBirth = dateOfBirth;
            AccountNumber = accountNumber;
            Zip = zip;
            LastName = lastName;
        }

        /// <summary>
        /// The recipient's date of birth
        /// </summary>
        [JsonConverter(typeof(DateTimeFormatConverter), "yyyy-MM-dd")]
        [JsonProperty("dob")]
        public DateTime DateOfBirth { get; }
        
        /// <summary>
        /// Gets the primary recipient's account number according to the source card scheme rules.
        /// </summary>
        public string AccountNumber { get; }
        
        /// <summary>
        /// Gets the first part of the primary recipient's Zip or postal code.
        /// </summary>
        public string Zip { get; }
        
        /// <summary>
        /// Gets the first 6 characters of the primary recipient's last name.
        /// </summary>
        public string LastName { get; }
    }
}