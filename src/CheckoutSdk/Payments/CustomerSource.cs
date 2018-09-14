using System;
using System.Linq;

namespace Checkout.Payments
{
    public class CustomerSource : IPaymentSource
    {
        public const string TypeName = "customer";

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

        public string Id { get; }
        public string Email { get; }
        public string Name { get; set; }
        public string Type => TypeName;
    }
}
