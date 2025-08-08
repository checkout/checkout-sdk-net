using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.KnetSource
{
    public enum LanguageType
    {
        [EnumMember(Value = "ar")]
        Ar,

        [EnumMember(Value = "en")]
        En,
    }
}