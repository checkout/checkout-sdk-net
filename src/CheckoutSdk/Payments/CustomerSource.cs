using System;
using System.Linq;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents a customer source for a payment request. The customer's default source of payment will be used.
    /// </summary>
    public class CustomerSource : IRequestSource
    {
        public const string TypeName = "customer";

        /// <summary>
        /// Creates a new <see cref="CustomerSource"/> instance with the provided id, email or both.
        /// </summary>
        /// <param name="id">The customer identifier, required if email is not provided.</param>
        /// <param name="email">The customer email address, required if id is not provided.</param>
        public CustomerSource(string id, string email)
        {
            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Either the customer id or email is required.");
            
            if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
                throw new FormatException($"The provided customer email {email} is invalid.");

            Email = email;
            Id = id;
        }

        /// <summary>
        /// Gets the customer identifier, required if email is not provided.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the customer email address, required if id is not provided.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the type of source
        /// </summary>
        public string Type => TypeName;

        /// <summary>
        /// Primitive email validation as geography-specific validation performed by API
        /// (Japanese email addresses do not always follow ISO rules)
        /// </summary>
        /// <param name="email">The email to validate</param>
        /// <returns>True if the email is valid, otherwise False.</returns>
        private static bool IsValidEmail(string email)
        {
            string[] parts = email?.Split('@');
            return parts.Length == 2 && !parts.Any(s => string.IsNullOrWhiteSpace(s));   
        }
    }
}
