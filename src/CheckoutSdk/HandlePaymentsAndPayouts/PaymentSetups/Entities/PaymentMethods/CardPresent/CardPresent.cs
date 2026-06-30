namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Card Present payment method's details and configuration.
    /// </summary>
    public class CardPresent : PaymentMethodBase
    {
        /// <summary>
        /// The Track 2 data read from card or device.
        /// [Optional] writeOnly
        /// </summary>
        public string Track2 { get; set; }

        /// <summary>
        /// The EMV data read from the card or device.
        /// [Optional] writeOnly
        /// </summary>
        public string Emv { get; set; }

        /// <summary>
        /// The mode used to capture the card details at the point of sale.
        /// [Optional] writeOnly
        /// </summary>
        public string EntryMode { get; set; }

        /// <summary>
        /// The encrypted PIN block details.
        /// [Optional] writeOnly
        /// </summary>
        public CardPresentPin Pin { get; set; }

        /// <summary>
        /// Set to true if you intend to reuse the payment credentials in subsequent payments.
        /// [Optional] writeOnly
        /// </summary>
        public bool? StoreForFutureUse { get; set; }

        /// <summary>
        /// The cardholder's name.
        /// [Optional] writeOnly
        /// </summary>
        public string Name { get; set; }
    }
}
