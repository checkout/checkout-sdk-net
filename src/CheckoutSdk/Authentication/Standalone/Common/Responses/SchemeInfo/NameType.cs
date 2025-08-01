using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.SchemeInfo
{
    public enum NameType
    {
        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,

        [EnumMember(Value = "visa")]
        Visa,

        [EnumMember(Value = "mastercard")]
        Mastercard,
    }
}