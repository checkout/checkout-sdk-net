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
        /// <param name="name">Dynamic descriptor name.</param>
        /// <param name="city">Dynamic descriptor city.</param>
        public BillingDescriptor(string name, string city)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The billing descriptor name is required.", nameof(name));

            if (name.Length > NameMaxLength)
                throw new ArgumentNullException($"The billing descriptor name cannot exceed {NameMaxLength} characters.", nameof(name));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("The billing descriptor city is required.", nameof(city));

            if (city.Length > CityMaxLength)
                throw new ArgumentNullException($"The billing descriptor city cannot exceed {CityMaxLength} characters.", nameof(city));

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