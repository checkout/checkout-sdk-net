using System;
using System.Linq;

namespace Checkout.Payments
{
    public class CustomerSource : IPaymentSource
    {
        public const string TypeName = "customer";

        /// <summary>
        /// Customer source of the payment
        /// </summary>
        /// <param name="id">The customer identifier, required if email is not provided</param>
        /// <param name="email">The customer email address, required if id is not provided</param>
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
        /// The customer identifier, required if email is not provided
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// The customer email address, required if id is not provided
        /// </summary>
        public string Email { get; }
        public string Type => TypeName;
    }
}
