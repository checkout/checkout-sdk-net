using Checkout.Common;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ShippingAddress
{
    /// <summary>
    /// shipping_address
    /// The shipping address. Any special characters will be replaced.
    /// </summary>
    public class ShippingAddress
    {
        /// <summary>
        /// The first line of the address
        /// [Optional]
        /// &lt;= 50
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address
        /// [Optional]
        /// &lt;= 50
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address
        /// [Optional]
        /// &lt;= 50
        /// </summary>
        public string AddressLine3 { get; set; }

        /// <summary>
        /// The address city
        /// [Optional]
        /// &lt;= 50
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The country subdivision code defined in ISO 3166-2
        /// [Optional]
        /// 3 characters
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The address zip/postal code
        /// [Optional]
        /// &lt;= 16
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the address
        /// [Optional]
        /// 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}