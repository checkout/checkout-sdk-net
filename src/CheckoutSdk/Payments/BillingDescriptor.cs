using System;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the billing descriptor to be displayed on the account owner's statement.
    /// </summary>
    public class BillingDescriptor
    {
        private const int NameMaxLength = 25;
        private const int CityMaxLength = 13;

        /// <summary>
        /// An optional dynamic billing descriptor displayed on the account owner's statement.
        /// </summary>
        /// <param name="name">The dynamic descriptor name. If the provided value exceeds 25 characters it will be trimmed automatically.</param>
        /// <param name="city">The dynamic descriptor city. If the provided value exceeds 13 characters it will be trimmed automatically.</param>
        public BillingDescriptor(string name, string city)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The billing descriptor name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("The billing descriptor city is required.", nameof(city));

            Name = name.Trim(NameMaxLength);
            City = city.Trim(CityMaxLength);
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