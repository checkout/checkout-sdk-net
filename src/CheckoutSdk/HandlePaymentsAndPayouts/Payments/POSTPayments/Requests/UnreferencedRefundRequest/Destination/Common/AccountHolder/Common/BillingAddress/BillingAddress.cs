using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.BillingAddress
{
    /// <summary>
    /// billing_address
    /// The account holder's billing address.
    /// If your company is incorporated in the United States, this field is required for all unreferenced refunds you
    /// perform.
    /// </summary>
    public class BillingAddress
    {
        /// <summary>
        /// The first line of the address.
        /// [Optional]
        /// <= 200
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// [Optional]
        /// <= 200
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The address city.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address state.
        /// [Optional]
        /// [ 2 .. 3 ] characters
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The address ZIP or postal code.
        /// You must provide US ZIPs in the format 00000, or 00000-0000.
        /// [Optional]
        /// <= 10
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The address country, as a two-letter ISO country code.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}