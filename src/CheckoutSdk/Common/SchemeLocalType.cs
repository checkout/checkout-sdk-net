using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum SchemeLocalType
    {
        /// <summary>Accel PINless debit network (US).</summary>
        [EnumMember(Value = "accel")]
        Accel,

        /// <summary>Cartes Bancaires local scheme (France).</summary>
        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        /// <summary>mada local scheme (Saudi Arabia).</summary>
        [EnumMember(Value = "mada")]
        Mada,

        /// <summary>NYCE PINless debit network (US).</summary>
        [EnumMember(Value = "nyce")]
        Nyce,

        /// <summary>OmanNet local scheme (Oman).</summary>
        [EnumMember(Value = "omannet")]
        Omannet,

        /// <summary>Pulse PINless debit network (US).</summary>
        [EnumMember(Value = "pulse")]
        Pulse,

        /// <summary>Shazam PINless debit network (US).</summary>
        [EnumMember(Value = "shazam")]
        Shazam,

        /// <summary>Star PINless debit network (US).</summary>
        [EnumMember(Value = "star")]
        Star,

        /// <summary>UnionPay International local scheme (China).</summary>
        [EnumMember(Value = "upi")]
        Upi,

        /// <summary>PayPak local scheme (Pakistan).</summary>
        [EnumMember(Value = "paypak")]
        Paypak,

        /// <summary>Maestro local scheme (international).</summary>
        [EnumMember(Value = "maestro")]
        Maestro,
    }
}
