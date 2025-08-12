using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.BillingAddress
{
    /// <summary>
    /// billing_address
    /// The payment source owner's billing address
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
        /// The second line of the address
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
        /// The state or province of the address country ISO 3166-2 code (for example: CA for California in the United
        /// States).
        /// [Optional]
        /// <= 3
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The address zip or postal code.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the address.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}