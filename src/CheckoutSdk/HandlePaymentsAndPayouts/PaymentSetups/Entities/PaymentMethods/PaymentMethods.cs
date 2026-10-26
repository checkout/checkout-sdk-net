using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The payment methods to configure for the payment setup.
    /// </summary>
    public class PaymentMethods
    {
        /// <summary>
        /// The payment method that automatically confirms the payment when it is ready, for example card.
        /// [Optional]
        /// </summary>
        public string AutoConfirm { get; set; }

        /// <summary>
        /// The instrument payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public PaymentSetupInstrument Instrument { get; set; }

        /// <summary>
        /// The Klarna payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Klarna Klarna { get; set; }

        /// <summary>
        /// The stc pay payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Stcpay Stcpay { get; set; }

        /// <summary>
        /// The Tabby payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Tabby Tabby { get; set; }

        /// <summary>
        /// The Bizum payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Bizum Bizum { get; set; }

        /// <summary>
        /// The PayPal payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Paypal Paypal { get; set; }

        /// <summary>
        /// The Blik payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Blik Blik { get; set; }

        /// <summary>
        /// The Bacs payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Bacs Bacs { get; set; }

        /// <summary>
        /// The Card Present payment method's details and configuration. The Payment Setups API specification
        /// removed card_present.
        /// [Optional]
        /// </summary>
        [Obsolete("card_present is no longer part of the Payment Setups API specification. There is no replacement. Will be removed in the next major version.")]
        public CardPresent CardPresent { get; set; }

        /// <summary>
        /// The Pay by Bank (Open Banking) payment method's details and configuration. The wire key is the single
        /// word paybybank, so it is set explicitly: the snake case naming strategy would turn PayByBank into
        /// pay_by_bank, which the API ignores.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "paybybank")]
        public PayByBank PayByBank { get; set; }

        /// <summary>
        /// The Stablecoin payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Stablecoin Stablecoin { get; set; }

        /// <summary>
        /// The Cash App Pay payment method's details and configuration. The wire key is the single word
        /// cashapp, so it is set explicitly: the snake case naming strategy would turn CashApp into
        /// cash_app.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "cashapp")]
        public CashApp CashApp { get; set; }

        /// <summary>
        /// The Google Pay payment method's details and configuration. The wire key is the single word
        /// googlepay, so it is set explicitly: the snake case naming strategy would turn GooglePay into
        /// google_pay.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "googlepay")]
        public GooglePay GooglePay { get; set; }

        /// <summary>
        /// The Apple Pay payment method's details and configuration. The wire key is the single word
        /// applepay, so it is set explicitly: the snake case naming strategy would turn ApplePay into
        /// apple_pay.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "applepay")]
        public ApplePay ApplePay { get; set; }

        /// <summary>
        /// The Card payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// The PayNow payment method's details and configuration.
        /// The wire key is the single word paynow, so it is set explicitly: the snake case naming strategy
        /// would turn PayNow into pay_now.
        /// [Optional] readOnly
        /// </summary>
        [JsonProperty(PropertyName = "paynow")]
        public PayNow PayNow { get; set; }

        /// <summary>
        /// The QPay payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Qpay Qpay { get; set; }

        /// <summary>
        /// The EPS payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Eps Eps { get; set; }

        /// <summary>
        /// The iDEAL payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Ideal Ideal { get; set; }

        /// <summary>
        /// The KNET payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Knet Knet { get; set; }

        /// <summary>
        /// The Bancontact payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Bancontact Bancontact { get; set; }

        /// <summary>
        /// The Benefit payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Benefit Benefit { get; set; }

        /// <summary>
        /// The Vipps payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Vipps Vipps { get; set; }

        /// <summary>
        /// The Twint payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Twint Twint { get; set; }

        /// <summary>
        /// The Alipay CN payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public AlipayCn AlipayCn { get; set; }

        /// <summary>
        /// The Alipay HK payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public AlipayHk AlipayHk { get; set; }

        /// <summary>
        /// The GCash payment method's details and configuration.
        /// The wire key is the single word gcash, so it is set explicitly: the snake case naming strategy
        /// would turn GCash into g_cash.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "gcash")]
        public GCash GCash { get; set; }

        /// <summary>
        /// The TNG payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Tng Tng { get; set; }

        /// <summary>
        /// The Dana payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Dana Dana { get; set; }

        /// <summary>
        /// The MobilePay payment method's details and configuration.
        /// The wire key is the single word mobilepay, so it is set explicitly: the snake case naming strategy
        /// would turn MobilePay into mobile_pay.
        /// [Optional] readOnly
        /// </summary>
        [JsonProperty(PropertyName = "mobilepay")]
        public MobilePay MobilePay { get; set; }

        /// <summary>
        /// The Tamara payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Tamara Tamara { get; set; }

        /// <summary>
        /// The MBWay payment method's details and configuration.
        /// The wire key is the single word mbway, so it is set explicitly: the snake case naming strategy
        /// would turn MbWay into mb_way.
        /// [Optional] readOnly
        /// </summary>
        [JsonProperty(PropertyName = "mbway")]
        public MbWay MbWay { get; set; }

        /// <summary>
        /// The Multibanco payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Multibanco Multibanco { get; set; }

        /// <summary>
        /// The WeChatPay payment method's details and configuration.
        /// The wire key is the single word wechatpay, so it is set explicitly: the snake case naming strategy
        /// would turn WeChatPay into we_chat_pay.
        /// [Optional] readOnly
        /// </summary>
        [JsonProperty(PropertyName = "wechatpay")]
        public WeChatPay WeChatPay { get; set; }

        /// <summary>
        /// The KakaoPay payment method's details and configuration.
        /// The wire key is the single word kakaopay, so it is set explicitly: the snake case naming strategy
        /// would turn KakaoPay into kakao_pay.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "kakaopay")]
        public KakaoPay KakaoPay { get; set; }

        /// <summary>
        /// The TrueMoney payment method's details and configuration.
        /// The wire key is the single word truemoney, so it is set explicitly: the snake case naming strategy
        /// would turn TrueMoney into true_money.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "truemoney")]
        public TrueMoney TrueMoney { get; set; }

        /// <summary>
        /// The Octopus payment method's details and configuration.
        /// [Optional] readOnly
        /// </summary>
        public Octopus Octopus { get; set; }

        /// <summary>
        /// The P24 (Przelewy24) payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public P24 P24 { get; set; }

        /// <summary>
        /// The Alma payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Alma Alma { get; set; }

        /// <summary>
        /// The Swish payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Swish Swish { get; set; }

        /// <summary>
        /// The Sequra payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Sequra Sequra { get; set; }

        /// <summary>
        /// The ACH payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Ach Ach { get; set; }

        /// <summary>
        /// The SEPA payment method's details and configuration.
        /// [Optional]
        /// </summary>
        public Sepa Sepa { get; set; }
    }
}
