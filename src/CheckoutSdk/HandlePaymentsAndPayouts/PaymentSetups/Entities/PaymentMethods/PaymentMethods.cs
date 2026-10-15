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
        /// [Optional]
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
        /// The Card Present payment method's details and configuration.
        /// [Optional]
        /// </summary>
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
    }
}
