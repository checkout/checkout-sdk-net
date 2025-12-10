

using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "alipay_cn")]   
        AlipayCn,
    
        [EnumMember(Value = "alipay_hk")]
        AlipayHk,
        
        [EnumMember(Value = "alma")]
        Alma,
        
        [EnumMember(Value = "apple_pay")]
        ApplePay,
        
        [EnumMember(Value = "bancontact")]
        Bancontact,
        
        [EnumMember(Value = "benefit")]
        Benefit,
        
        [EnumMember(Value = "bizum")]
        Bizum,
        
        [EnumMember(Value = "card")]
        Card,
        
        [EnumMember(Value = "dana")]
        Dana,
        
        [EnumMember(Value = "eps")]
        Eps,
        
        [EnumMember(Value = "gcash")]
        GCash,
        
        [EnumMember(Value = "googlepay")]
        GooglePay,
        
        [EnumMember(Value = "ideal")]
        Ideal,
        
        [EnumMember(Value = "kakaopay")]
        KakaoPay,
        
        [EnumMember(Value = "klarna")]
        Klarna,
        
        [EnumMember(Value = "knet")]
        Knet,
        
        [EnumMember(Value = "mbway")]
        MbWay,
        
        [EnumMember(Value = "mobilepay")]
        MobilePay,
        
        [EnumMember(Value = "multibanco")]
        Multibanco,
        
        [EnumMember(Value = "octopus")]
        Octopus,
        
        [EnumMember(Value = "p24")]
        P24,
        
        [EnumMember(Value = "paynow")]
        PayNow,
        
        [EnumMember(Value = "paypal")]
        PayPal,
        
        [EnumMember(Value = "plaid")]
        Plaid,
        
        [EnumMember(Value = "qpay")]
        QPay,
        
        [EnumMember(Value = "remember_me")]
        RememberMe,
        
        [EnumMember(Value = "sepa")]
        Sepa,
        
        [EnumMember(Value = "stcpay")]
        StcPay,
        
        [EnumMember(Value = "stored_card")]
        StoredCard,
        
        [EnumMember(Value = "tabby")]
        Tabby,
        
        [EnumMember(Value = "tamara")]
        Tamara,
        
        [EnumMember(Value = "tng")]
        Tng,
        
        [EnumMember(Value = "truemoney")]
        TrueMoney,
        
        [EnumMember(Value = "twint")]
        Twint,
        
        [EnumMember(Value = "vipps")]
        Vipps,
        
        [EnumMember(Value = "wechatpay")]
        WeChatPay
    }
}