namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    BlikSource
{
    /// <summary>
    /// blik source Class.
    /// Use this to process Blik payments in Poland.
    /// When source.type is blik: currency must be PLN, amount must not exceed 5,000,000 (minor unit),
    /// reference is limited to 35 characters. For customer-initiated payments, provide the 6-digit
    /// Blik code in processing.partner_code. For merchant-initiated recurring payments, use either
    /// source.type: id with a previous source.id, or source.type: blik with partner_agreement_id.
    /// </summary>
    public class BlikSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the BlikSource class.
        /// </summary>
        public BlikSource() : base(SourceType.Blik)
        {
        }

        /// <summary>
        /// The Checkout.com source identifier for the partner agreement created during a Blik recurring payment.
        /// Use this value as source.id (with source.type: id) to process subsequent merchant-initiated payments
        /// against the same agreement.
        /// [Optional] response-only
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Blik PAYID identifying an external partner agreement created with another PSP.
        /// Only used when processing merchant-initiated recurring payments (merchant_initiated: true)
        /// without a stored Checkout.com source.
        /// [Optional]
        /// &lt;= 64 characters
        /// </summary>
        public string PartnerAgreementId { get; set; }
    }
}
