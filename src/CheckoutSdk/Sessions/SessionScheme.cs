using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionScheme
    {
        [EnumMember(Value = "amex")] Amex,

        [EnumMember(Value = "cartes_bancaires")]
        CartesBancaires,
        
        [EnumMember(Value = "diners")] Diners,
        [EnumMember(Value = "jcb")] Jcb,
        [EnumMember(Value = "mastercard")] Mastercard,
        [EnumMember(Value = "visa")] Visa
    }
}