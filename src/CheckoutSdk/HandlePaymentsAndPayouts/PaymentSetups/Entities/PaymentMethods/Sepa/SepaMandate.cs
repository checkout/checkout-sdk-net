using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The mandate details.
    /// </summary>
    public class SepaMandate
    {
        /// <summary>
        /// The ID of the mandate.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The type of mandate.
        /// [Optional]
        /// </summary>
        public PaymentSetupSepaMandateType? Type { get; set; }

        /// <summary>
        /// The date the mandate was signed.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? DateOfSignature { get; set; }
    }
}
