using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.Phone
{
    /// <summary>
    /// phone
    /// The account holder's phone number.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// The international dialing code for the account holder's address country, as an ITU-T E.164 code.
        /// [Required]
        /// [ 1 .. 7 ] characters
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// The digits of the phone number, not including the country_code.
        /// [Required]
        /// [ 6 .. 25 ] characters
        /// </summary>
        public string Number { get; set; }
    }
}