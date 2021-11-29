using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Apm.Klarna
{
    public sealed class CreditSession : Resource, IEquatable<CreditSession>
    {
        public string ClientToken { get; set; }

        public string PurchaseCountry { get; set; }

        public string Currency { get; set; }

        public string Locale { get; set; }

        public long? Amount { get; set; }

        public int? TaxAmount { get; set; }

        public List<KlarnaProduct> Products { get; set; }

        public bool Equals(CreditSession other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ClientToken == other.ClientToken && PurchaseCountry == other.PurchaseCountry &&
                   Currency == other.Currency && Locale == other.Locale && Amount == other.Amount &&
                   TaxAmount == other.TaxAmount && Equals(Products, other.Products);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreditSession other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientToken, PurchaseCountry, Currency, Locale, Amount, TaxAmount, Products);
        }
    }
}