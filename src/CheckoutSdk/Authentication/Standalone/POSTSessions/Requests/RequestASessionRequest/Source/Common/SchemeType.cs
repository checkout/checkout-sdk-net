using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common
{
    public enum SchemeType
    {
        [EnumMember(Value = "amex")]
        Amex,

        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        [EnumMember(Value = "diners")]
        Diners,

        [EnumMember(Value = "mastercard")]
        Mastercard,

        [EnumMember(Value = "visa")]
        Visa,

        [EnumMember(Value = "discover")]
        Discover,

        [EnumMember(Value = "upi")]
        Upi,

        [EnumMember(Value = "jcb")]
        Jcb,
    }
}