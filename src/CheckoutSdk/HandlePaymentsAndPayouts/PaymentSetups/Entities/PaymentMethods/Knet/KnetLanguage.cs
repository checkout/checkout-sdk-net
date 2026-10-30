using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's preferred language, as an ISO 639-1 code: ar for Arabic, en for English.
    /// </summary>
    public enum KnetLanguage
    {
        [EnumMember(Value = "en")]
        En,

        [EnumMember(Value = "ar")]
        Ar
    }
}
