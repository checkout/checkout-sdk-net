using System;

namespace Checkout.Payments.Four.Response
{
    public sealed class PaymentResponseBalances : IEquatable<PaymentResponseBalances>
    {
        public long? TotalAuthorized { get; set; }

        public long? TotalVoided { get; set; }

        public long? AvailableToVoid { get; set; }

        public long? TotalCaptured { get; set; }

        public long? AvailableToCapture { get; set; }

        public long? TotalRefunded { get; set; }

        public long? AvailableToRefund { get; set; }

        public bool Equals(PaymentResponseBalances other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TotalAuthorized == other.TotalAuthorized && TotalVoided == other.TotalVoided &&
                   AvailableToVoid == other.AvailableToVoid && TotalCaptured == other.TotalCaptured &&
                   AvailableToCapture == other.AvailableToCapture && TotalRefunded == other.TotalRefunded &&
                   AvailableToRefund == other.AvailableToRefund;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentResponseBalances other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TotalAuthorized, TotalVoided, AvailableToVoid, TotalCaptured, AvailableToCapture,
                TotalRefunded, AvailableToRefund);
        }
    }
}