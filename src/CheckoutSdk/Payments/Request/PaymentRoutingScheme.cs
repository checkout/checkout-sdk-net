using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    public enum PaymentRoutingScheme
    {
        /// <summary>Accel PINless debit network (US).</summary>
        [EnumMember(Value = "accel")]
        Accel,

        /// <summary>American Express.</summary>
        [EnumMember(Value = "amex")]
        Amex,

        /// <summary>Cartes Bancaires local scheme (France).</summary>
        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        /// <summary>Diners Club International.</summary>
        [EnumMember(Value = "diners")]
        Diners,

        /// <summary>Discover Network.</summary>
        [EnumMember(Value = "discover")]
        Discover,

        /// <summary>JCB (Japan Credit Bureau).</summary>
        [EnumMember(Value = "jcb")]
        Jcb,

        /// <summary>mada local scheme (Saudi Arabia).</summary>
        [EnumMember(Value = "mada")]
        Mada,

        /// <summary>Maestro local scheme.</summary>
        [EnumMember(Value = "maestro")]
        Maestro,

        /// <summary>Mastercard.</summary>
        [EnumMember(Value = "mastercard")]
        Mastercard,

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

        /// <summary>UnionPay International (China).</summary>
        [EnumMember(Value = "upi")]
        Upi,

        /// <summary>Visa.</summary>
        [EnumMember(Value = "visa")]
        Visa,
    }
}
