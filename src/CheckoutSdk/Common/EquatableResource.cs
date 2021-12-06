using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Common
{
    public abstract class EquatableResource<T> : Resource, IEquatable<T> where T : IEquatable<T>
    {
        public abstract bool EqualExp(T other);

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualExp(other);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is T other && Equals(other);
        }

        public abstract override int GetHashCode();
    }
}