using Newtonsoft.Json;
using System;

namespace Checkout.Payments
{
    public class PaymentRecipient
    {
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

        [JsonConverter(typeof(DateTimeFormatConverter), "yyyy-MM-dd")]
        public DateTime Dob { get; }
        public string AccountNumber { get; }
        public string Zip { get; }
        public string LastName { get; }
    }
}