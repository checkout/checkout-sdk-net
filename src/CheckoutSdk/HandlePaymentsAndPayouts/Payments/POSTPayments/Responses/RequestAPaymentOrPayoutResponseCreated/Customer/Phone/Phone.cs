using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Customer.
    Phone
{
    /// <summary>
    /// phone
    /// The customer's phone number.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// The international country calling code. Required if source.type is tamara.
        /// [Optional]
        /// [ 1 .. 7 ] characters
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// The phone number. Required if source.type is tamara.
        /// [Optional]
        /// [ 6 .. 25 ] characters
        /// </summary>
        public string Number { get; set; }
    }
}