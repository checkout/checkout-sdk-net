using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestKlarnaSource : AbstractRequestSource, IEquatable<RequestKlarnaSource>
    {
        public RequestKlarnaSource() : base(PaymentSourceType.Klarna)
        {
        }

        public string AuthorizationToken { get; set; }

        public string Locale { get; set; }

        public CountryCode? PurchaseCountry { get; set; }

        public long? TaxAmount { get; set; }

        public Address BillingAddress { get; set; }

        public KlarnaCustomer Customer { get; set; }

        public List<KlarnaCustomer> Products { get; set; }

        public bool Equals(RequestKlarnaSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AuthorizationToken == other.AuthorizationToken && Locale == other.Locale &&
                   PurchaseCountry == other.PurchaseCountry && TaxAmount == other.TaxAmount &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Customer, other.Customer) &&
                   Equals(Products, other.Products);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestKlarnaSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorizationToken, Locale, PurchaseCountry, TaxAmount, BillingAddress, Customer,
                Products);
        }
    }
}