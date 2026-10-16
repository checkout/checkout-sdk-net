using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// A card scheme name supported by a Payment Setup payment method.
    /// </summary>
    public enum PaymentSetupScheme
    {
        [EnumMember(Value = "accel")]
        Accel,

        [EnumMember(Value = "amex")]
        Amex,

        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        [EnumMember(Value = "china_union_pay")]
        ChinaUnionPay,

        [EnumMember(Value = "diners")]
        Diners,

        [EnumMember(Value = "discover")]
        Discover,

        [EnumMember(Value = "jcb")]
        Jcb,

        [EnumMember(Value = "mada")]
        Mada,

        [EnumMember(Value = "maestro")]
        Maestro,

        [EnumMember(Value = "mastercard")]
        Mastercard,

        [EnumMember(Value = "nyce")]
        Nyce,

        [EnumMember(Value = "omannet")]
        Omannet,

        [EnumMember(Value = "pulse")]
        Pulse,

        [EnumMember(Value = "shazam")]
        Shazam,

        [EnumMember(Value = "star")]
        Star,

        [EnumMember(Value = "upi")]
        Upi,

        [EnumMember(Value = "visa")]
        Visa
    }
}
