using System;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the billing descriptor to be displayed on the account owner's statement.
    /// </summary>
    public class BillingDescriptor
    {
        /// <summary>
        /// An optional dynamic billing descriptor displayed on the account owner's statement.
        /// </summary>
        /// <param name="name">The dynamic descriptor name. If the provided value exceeds 25 characters it will be trimmed automatically.</param>
        /// <param name="city">The dynamic descriptor city. If the provided value exceeds 13 characters it will be trimmed automatically.</param>
        public BillingDescriptor(string name, string city)
        {
            Name = name;
            City = city;
        }

        /// <summary>
        /// Gets the dynamic descriptor name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the dynamic descriptor city.
        /// </summary>
        public string City { get; }
    }
}