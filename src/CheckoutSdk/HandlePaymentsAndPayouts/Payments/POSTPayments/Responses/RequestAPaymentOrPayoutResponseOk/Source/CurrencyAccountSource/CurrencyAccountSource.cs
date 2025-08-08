using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseOk.Source;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.CurrencyAccountSource
{
    /// <summary>
    /// currency_account source Class
    /// The source of the payment
    /// </summary>
    public class CurrencyAccountSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the CurrencyAccountSource class.
        /// </summary>
        public CurrencyAccountSource() : base(AccountSourceType.Currency)
        {
        }

        /// <summary>
        /// The ID of the currency account
        /// [Required]
        /// ^(ca)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// If specified, indicates the amount in the source currency to be paid out. If omitted, the root amount in the
        /// destination currency will be used.The amount must be provided in the minor currency unit.
        /// [Optional]
        /// </summary>
        public int Amount { get; set; }

    }
}
