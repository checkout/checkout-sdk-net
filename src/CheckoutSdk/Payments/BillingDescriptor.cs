using System;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class BillingDescriptor
    {
        private const int MaxNameLength = 25;
        private const int MaxCityLength = 13;

        [JsonConstructor]
        private BillingDescriptor() { }

        /// <summary>
        /// An optional dynamic billing descriptor displayed on the account owner's statement.
        /// </summary>
        /// <param name="name">Dynamic description of the charge</param>
        /// <param name="city">City where the charge originated</param>
        public BillingDescriptor(string name, string city)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
                throw new ArgumentException($"{nameof(name)} cannot be null or whitespace and its length cannot exceed {MaxNameLength}.");

            Name = name;

            if (string.IsNullOrWhiteSpace(city) || city.Length > MaxCityLength)
                throw new ArgumentException($"{nameof(city)} cannot be null or whitespace and its length cannot exceed {MaxCityLength}.");
            City = city;
        }
        /// <summary>
        /// Dynamic description of the charge
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// City where the charge originated
        /// </summary>
        public string City { get; }
    }
}