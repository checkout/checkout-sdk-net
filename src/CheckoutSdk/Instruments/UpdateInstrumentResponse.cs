using System;

namespace Checkout.Instruments
{
    public sealed class UpdateInstrumentResponse : IEquatable<UpdateInstrumentResponse>
    {
        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }

        public bool Equals(UpdateInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Fingerprint == other.Fingerprint;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Type, Fingerprint);
        }
    }
}