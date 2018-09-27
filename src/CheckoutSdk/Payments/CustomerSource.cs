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
            if (!string.IsNullOrWhiteSpace(email))
            {
                string[] split = email?.Split('@');
                if (split.Length != 2 || split.Any(string.IsNullOrWhiteSpace))
                {
                    throw new FormatException($"{email} contain one @");
                }

                Email = email;
            }

            if (string.IsNullOrWhiteSpace(id)
                && string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"Either {nameof(id)} or {nameof(email)} must be provided.");
            }

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
    }
}
