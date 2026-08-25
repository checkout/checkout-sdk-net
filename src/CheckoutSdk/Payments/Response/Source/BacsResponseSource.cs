using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Response.Source
{
    /// <summary>
    /// Bacs Direct Debit source.
    /// This source declares only a type and an id, so it does not derive from
    /// AbstractResponseSource, which would add a billing address and a phone number that the
    /// API does not return for Bacs Direct Debit.
    /// </summary>
    public class BacsResponseSource : IResponseSource
    {
        /// <summary>
        /// The payment source type.
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public PaymentSourceType? SourceType { get; set; }

        /// <summary>
        /// The instrument ID.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The payment source type, exposed through IResponseSource so callers can discriminate the
        /// source without casting.
        /// </summary>
        public PaymentSourceType? Type()
        {
            return SourceType;
        }
    }
}
