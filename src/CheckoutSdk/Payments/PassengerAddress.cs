using System;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Contains information about the passenger's address.
    /// </summary>
    public class PassengerAddress
    {
        /// <summary>
        /// The two-letter ISO country code of the passenger's country of residence.
        /// [Optional]
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
