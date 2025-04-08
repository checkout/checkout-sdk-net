using System.Runtime.Serialization;

namespace Checkout.Payments.Sessions
{
    public enum PaymentMethodsType
    {
        [EnumMember(Value = "alipay_cn")] AlipayCN,
        [EnumMember(Value = "alipay_hk")] AlipayHK,
        [EnumMember(Value = "alma")] Alma,
        [EnumMember(Value = "applepay")] Applepay,
        [EnumMember(Value = "bancontact")] Bancontact,
        [EnumMember(Value = "benefit")] Benefit,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "dana")] Dana,
        [EnumMember(Value = "eps")] EPS,
        [EnumMember(Value = "gcash")] GCash,
        [EnumMember(Value = "giropay")] Giropay,
        [EnumMember(Value = "googlepay")] Googlepay,
        [EnumMember(Value = "ideal")] Ideal,
        [EnumMember(Value = "kakaopay")] KakaoPay,
        [EnumMember(Value = "klarna")] Klarna,
        [EnumMember(Value = "knet")] KNet,
        [EnumMember(Value = "mbway")] MBWay,
        [EnumMember(Value = "multibanco")] Multibanco,
        [EnumMember(Value = "p24")] Przelewy24,
        [EnumMember(Value = "paypal")] PayPal,
        [EnumMember(Value = "qpay")] QPay,
        [EnumMember(Value = "sepa")] SEPA,
        [EnumMember(Value = "sofort")] Sofort,
        [EnumMember(Value = "stcpay")] STCPay,
        [EnumMember(Value = "tabby")] Tabby,
        [EnumMember(Value = "tamara")] Tamara,
        [EnumMember(Value = "tng")] TNG,
        [EnumMember(Value = "truemoney")] TrueMoney,
    }
}