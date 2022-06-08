using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum PreferredSchema
    {
        [EnumMember(Value = "visa")] Visa,
        [EnumMember(Value = "mastercard")] Mastercard,
        [EnumMember(Value = "cartes_bancaires")] CartesBancaires
    }
}