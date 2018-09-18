using System;

namespace Checkout.Payments
{
    public class BillingDescriptor
    {
        private const int MaxNameLength = 25;
        private const int MaxCityLength = 13;

        public BillingDescriptor(string name, string city)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
                throw new ArgumentException($"{nameof(name)} cannot be null or whitespace and its length cannot exceed {MaxNameLength}.");

            Name = name;

            if (string.IsNullOrWhiteSpace(city) || city.Length > MaxCityLength)
                throw new ArgumentException($"{nameof(city)} cannot be null or whitespace and its length cannot exceed {MaxCityLength}.");
            City = city;
        }

        public string Name { get; }
        public string City { get; }
    }
}