using System.Runtime.Serialization;

namespace Checkout.Payments.Sessions
{
    public enum PaymentMethodsType
    {
        [EnumMember(Value = "applepay")] Applepay,
        [EnumMember(Value = "bancontact")] Bancontact,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "eps")] EPS,
        [EnumMember(Value = "giropay")] Giropay,
        [EnumMember(Value = "googlepay")] Googlepay,
        [EnumMember(Value = "ideal")] Ideal,
        [EnumMember(Value = "knet")] KNet,
        [EnumMember(Value = "multibanco")] Multibanco,
        [EnumMember(Value = "p24")] Przelewy24,
        [EnumMember(Value = "paypal")] PayPal,
        [EnumMember(Value = "sofort")] Sofort,
    }
}