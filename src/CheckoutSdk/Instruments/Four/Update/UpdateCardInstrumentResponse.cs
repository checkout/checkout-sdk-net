using System;

namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateCardInstrumentResponse : UpdateInstrumentResponse,
        IEquatable<UpdateCardInstrumentResponse>
    {
        public UpdateCardInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }

        public bool Equals(UpdateCardInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Fingerprint == other.Fingerprint;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateCardInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Fingerprint != null ? Fingerprint.GetHashCode() : 0);
        }
    }
}