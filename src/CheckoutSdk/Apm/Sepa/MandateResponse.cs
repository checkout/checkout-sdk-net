using System;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Apm.Sepa
{
    public sealed class MandateResponse : Resource, IEquatable<MandateResponse>
    {
        public string MandateReference { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonProperty(PropertyName = "address_line1")]
        public string AddressLine1 { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public CountryCode? Country { get; set; }

        public string MaskedAccountIban { get; set; }

        public string AccountCurrencyCode { get; set; }

        public CountryCode? AccountCountryCode { get; set; }

        public string MandateState { get; set; }

        public string BillingDescriptor { get; set; }

        public string MandateType { get; set; }

        public bool Equals(MandateResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MandateReference == other.MandateReference && CustomerId == other.CustomerId &&
                   FirstName == other.FirstName && LastName == other.LastName && AddressLine1 == other.AddressLine1 &&
                   City == other.City && Zip == other.Zip && Country == other.Country &&
                   MaskedAccountIban == other.MaskedAccountIban && AccountCurrencyCode == other.AccountCurrencyCode &&
                   AccountCountryCode == other.AccountCountryCode && MandateState == other.MandateState &&
                   BillingDescriptor == other.BillingDescriptor && MandateType == other.MandateType;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is MandateResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(MandateReference);
            hashCode.Add(CustomerId);
            hashCode.Add(FirstName);
            hashCode.Add(LastName);
            hashCode.Add(AddressLine1);
            hashCode.Add(City);
            hashCode.Add(Zip);
            hashCode.Add(Country);
            hashCode.Add(MaskedAccountIban);
            hashCode.Add(AccountCurrencyCode);
            hashCode.Add(AccountCountryCode);
            hashCode.Add(MandateState);
            hashCode.Add(BillingDescriptor);
            hashCode.Add(MandateType);
            return hashCode.ToHashCode();
        }
    }
}