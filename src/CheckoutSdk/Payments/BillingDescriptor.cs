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
        /// <param name="name">Dynamic description of the charge</param>
        /// <param name="city">City where the charge originated</param>
        public BillingDescriptor(string name, string city)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The billing descriptor name is required.", nameof(name));

            if (name.Length > NameMaxLength)
                throw new ArgumentNullException($"The billing descriptor name cannot exceed {NameMaxLength} characters.", nameof(name));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("The billing descriptor city is required.", nameof(city));

            if (city.Length > NameMaxLength)
                throw new ArgumentNullException($"The billing descriptor city cannot exceed {CityMaxLength} characters.", nameof(city));

            Name = name;
            City = city;
        }

        /// <summary>
        /// Dynamic description of the payment
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// City where the payment originated
        /// </summary>
        public string City { get; }
    }
}