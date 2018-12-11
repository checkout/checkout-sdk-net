using Newtonsoft.Json;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines the source data of a <see cref="SourceRequest"/>.
    /// </summary>
    public class SourceData
    {
        /// <summary>
        /// Gets or sets the first name on the source data.
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name on the source data.
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the iban of the source data.
        /// </summary>
        [JsonProperty(PropertyName = "account_iban")]
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets the bic on the source data.
        /// </summary>
        [JsonProperty(PropertyName = "bic")]
        public string Bic { get; set; }

        /// <summary>
        /// Gets or sets the billing descriptor on the source data.
        /// </summary>
        [JsonProperty(PropertyName = "billing_descriptor")]
        public string BillingDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the mandate type on the source data.
        /// </summary>
        [JsonProperty(PropertyName = "mandate_type")]
        public string MandateType { get; set; }
    }
}
