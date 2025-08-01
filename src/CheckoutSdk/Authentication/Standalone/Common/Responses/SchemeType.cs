using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum SchemeType
    {
        [EnumMember(Value = "visa")]
        Visa,

        [EnumMember(Value = "mastercard")]
        Mastercard,

        [EnumMember(Value = "jcb")]
        Jcb,

        [EnumMember(Value = "amex")]
        Amex,

        [EnumMember(Value = "diners")]
        Diners,

        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        [EnumMember(Value = "discover")]
        Discover,

        [EnumMember(Value = "upi")]
        Upi,
    }
}