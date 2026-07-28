using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Blik source. Use this to process Blik payments in Poland.
    /// When source.type is blik: currency must be PLN, amount must not exceed 5,000,000
    /// (minor unit), and reference is limited to 35 characters.
    /// For customer-initiated payments (merchant_initiated: false), provide the customer's
    /// 6-digit Blik code in processing.partner_code. For merchant-initiated recurring payments
    /// (merchant_initiated: true), use either source.type: id with a previous source.id, or
    /// source.type: blik with partner_agreement_id.
    /// </summary>
    public class RequestBlikSource : AbstractRequestSource
    {
        /// <summary>
        /// The Blik PAYID identifying an external partner agreement created with another PSP.
        /// Only used for merchant-initiated recurring payments without a stored Checkout.com source.
        /// [Optional] max 64 characters
        /// </summary>
        public string PartnerAgreementId { get; set; }

        public RequestBlikSource() : base(PaymentSourceType.Blik)
        {
        }
    }
}
