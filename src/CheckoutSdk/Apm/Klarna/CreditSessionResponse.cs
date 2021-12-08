using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Apm.Klarna
{
    public sealed class CreditSessionResponse : Resource, IEquatable<CreditSessionResponse>
    {
        public string SessionId { get; set; }

        public string ClientToken { get; set; }

        public IList<PaymentMethodCategory> PaymentMethodCategories { get; set; }

        public bool Equals(CreditSessionResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SessionId == other.SessionId && ClientToken == other.ClientToken &&
                   Equals(PaymentMethodCategories, other.PaymentMethodCategories);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreditSessionResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SessionId, ClientToken, PaymentMethodCategories);
        }
    }
}