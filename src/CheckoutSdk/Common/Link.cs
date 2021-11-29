using System;

namespace Checkout.Common
{
    public sealed class Link : IEquatable<Link>
    {
        public string Href { get; set; }

        public string Title { get; set; }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Href == other.Href && Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Link) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Href, Title);
        }
    }
}