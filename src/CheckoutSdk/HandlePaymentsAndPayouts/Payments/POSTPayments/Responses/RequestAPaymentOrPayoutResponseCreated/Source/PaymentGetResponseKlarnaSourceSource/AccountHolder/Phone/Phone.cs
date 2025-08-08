using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    PaymentGetResponseKlarnaSourceSource.AccountHolder.Phone
{
    /// <summary>
    /// phone
    /// Phone number of the account holder.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// [Optional]
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string Number { get; set; }
    }
}