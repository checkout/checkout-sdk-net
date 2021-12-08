using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Apm.Klarna
{
    public sealed class CreditSessionRequest : IEquatable<CreditSessionRequest>
    {
        public CountryCode? PurchaseCountry { get; set; }

        public Currency? Currency { get; set; }

        public string Locale { get; set; }

        public long? Amount { get; set; }

        public int? TaxAmount { get; set; }

        public IList<KlarnaProduct> Products { get; set; }

        public bool Equals(CreditSessionRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PurchaseCountry == other.PurchaseCountry && Currency == other.Currency && Locale == other.Locale &&
                   Amount == other.Amount && TaxAmount == other.TaxAmount && Equals(Products, other.Products);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreditSessionRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PurchaseCountry, Currency, Locale, Amount, TaxAmount, Products);
        }
    }
}