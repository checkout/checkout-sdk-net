using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum PaymentSourceType
    {
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "id")] Id,
        [EnumMember(Value = "network_token")] NetworkToken,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "customer")] Customer,
        [EnumMember(Value = "provider_token")] ProviderToken,
        [EnumMember(Value = "dLocal")] DLocal,

        [EnumMember(Value = "currency_account")]
        CurrencyAccount,
        [EnumMember(Value = "baloto")] Baloto,
        [EnumMember(Value = "boleto")] Boleto,
        [EnumMember(Value = "fawry")] Fawry,
        [EnumMember(Value = "giropay")] Giropay,
        [EnumMember(Value = "ideal")] Ideal,
        [EnumMember(Value = "oxxo")] Oxxo,
        [EnumMember(Value = "pagofacil")] PagoFacil,
        [EnumMember(Value = "rapipago")] RapiPago,
        [EnumMember(Value = "klarna")] Klarna,
        [EnumMember(Value = "sofort")] Sofort,
        [EnumMember(Value = "knet")] KNet,
        [EnumMember(Value = "qpay")] QPay,
        [EnumMember(Value = "alipay")] Alipay,
        [EnumMember(Value = "paypal")] PayPal,
        [EnumMember(Value = "multibanco")] Multibanco,
        [EnumMember(Value = "eps")] EPS,
        [EnumMember(Value = "poli")] Poli,
        [EnumMember(Value = "p24")] Przelewy24,
        [EnumMember(Value = "benefitpay")] BenefitPay,
        [EnumMember(Value = "bancontact")] Bancontact,
        [EnumMember(Value = "tamara")] Tamara,
        [EnumMember(Value = "bank_account")] BankAccount,
        [EnumMember(Value = "alipay_hk")] AlipayHk,
        [EnumMember(Value = "alipay_cn")] AlipayCn,
        [EnumMember(Value = "gcash")] Gcash,
        [EnumMember(Value = "wechatpay")] Wechatpay
    }
}