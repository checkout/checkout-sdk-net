using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source
{
    public enum SourceType
    {
        [EnumMember(Value = "card")]
        Card,

        [EnumMember(Value = "afterpay")]
        Afterpay,

        [EnumMember(Value = "alipay_cn")]
        AlipayCn,

        [EnumMember(Value = "alipay_hk")]
        AlipayHk,

        [EnumMember(Value = "alipay_plus")]
        AlipayPlus,

        [EnumMember(Value = "alma")]
        Alma,

        [EnumMember(Value = "bancontact")]
        Bancontact,

        [EnumMember(Value = "benefit")]
        Benefit,

        [EnumMember(Value = "cvconnect")]
        Cvconnect,

        [EnumMember(Value = "dana")]
        Dana,

        [EnumMember(Value = "eps")]
        Eps,

        [EnumMember(Value = "fawry")]
        Fawry,

        [EnumMember(Value = "gcash")]
        Gcash,

        [EnumMember(Value = "ideal")]
        Ideal,

        [EnumMember(Value = "illicado")]
        Illicado,

        [EnumMember(Value = "kakaopay")]
        Kakaopay,

        [EnumMember(Value = "klarna")]
        Klarna,

        [EnumMember(Value = "knet")]
        Knet,

        [EnumMember(Value = "mbway")]
        Mbway,

        [EnumMember(Value = "mobilepay")]
        Mobilepay,

        [EnumMember(Value = "multibanco")]
        Multibanco,

        [EnumMember(Value = "octopus")]
        Octopus,

        [EnumMember(Value = "paynow")]
        Paynow,

        [EnumMember(Value = "paypal")]
        Paypal,

        [EnumMember(Value = "postfinance")]
        Postfinance,

        [EnumMember(Value = "p24")]
        P24,

        [EnumMember(Value = "qpay")]
        Qpay,

        [EnumMember(Value = "sepa")]
        Sepa,

        [EnumMember(Value = "sequra")]
        Sequra,

        [EnumMember(Value = "stcpay")]
        Stcpay,

        [EnumMember(Value = "tamara")]
        Tamara,

        [EnumMember(Value = "tng")]
        Tng,

        [EnumMember(Value = "truemoney")]
        Truemoney,

        [EnumMember(Value = "twint")]
        Twint,

        [EnumMember(Value = "vipps")]
        Vipps,

        [EnumMember(Value = "wechatpay")]
        Wechatpay,

        [EnumMember(Value = "currency_account")]
        CurrencyAccount,

        [EnumMember(Value = "PaymentGetResponseGiropaySource")]
        PaymentGetResponseGiropaySource,

        [EnumMember(Value = "PaymentGetResponseKlarnaSource")]
        PaymentGetResponseKlarnaSource,

        [EnumMember(Value = "PaymentGetResponseSEPAV4Source")]
        PaymentGetResponseSEPAVFourSource,

        [EnumMember(Value = "PaymentResponseSource")]
        PaymentResponseSource,
    }
}