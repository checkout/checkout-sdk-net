using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Apm.Klarna
{
    public sealed class OrderCaptureRequest : IEquatable<OrderCaptureRequest>
    {
        public const PaymentSourceType Type = PaymentSourceType.Klarna;

        public long? Amount { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public Klarna Klarna { get; set; }

        public KlarnaShippingInfo ShippingInfo { get; set; }

        public long? ShippingDelay { get; set; }

        public bool Equals(OrderCaptureRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Reference == other.Reference && Equals(Metadata, other.Metadata) &&
                   Equals(Klarna, other.Klarna) && Equals(ShippingInfo, other.ShippingInfo) &&
                   ShippingDelay == other.ShippingDelay;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is OrderCaptureRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Reference, Metadata, Klarna, ShippingInfo, ShippingDelay);
        }
    }
}