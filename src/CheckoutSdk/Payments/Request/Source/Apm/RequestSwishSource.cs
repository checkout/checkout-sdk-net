using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Swish request source.
    /// </summary>
    public class RequestSwishSource : AbstractRequestSource
    {
        public RequestSwishSource() : base(PaymentSourceType.Swish)
        {
        }

        /// <summary>
        /// The 2-letter ISO country code of the country in which the payment instrument is issued or
        /// operated.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// Enum: "SE"
        /// </summary>
        public CountryCode? PaymentCountry { get; set; }

        /// <summary>
        /// Information about the account holder's details.
        /// [Required]
        /// </summary>
        public SwishAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The payment billing descriptor.
        /// [Optional]
        /// </summary>
        public SwishBillingDescriptor BillingDescriptor { get; set; }
    }
}
