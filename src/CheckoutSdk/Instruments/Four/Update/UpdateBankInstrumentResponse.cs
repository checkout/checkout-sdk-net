using System;

namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateBankInstrumentResponse : UpdateInstrumentResponse,
        IEquatable<UpdateBankInstrumentResponse>
    {
        public UpdateBankInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public string Fingerprint { get; set; }

        public bool Equals(UpdateBankInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Fingerprint == other.Fingerprint;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateBankInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Fingerprint != null ? Fingerprint.GetHashCode() : 0);
        }
    }
}