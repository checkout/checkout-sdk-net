namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    FawrySource
{
    /// <summary>
    /// fawry source Class
    /// The source of the payment
    /// </summary>
    public class FawrySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the FawrySource class.
        /// </summary>
        public FawrySource() : base(SourceType.Fawry)
        {
        }

        /// <summary>
        /// Payment description
        /// [Required]
        /// &lt;= 65534
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The customer pays using this number at Fawry's outlets
        /// [Optional]
        /// </summary>
        public string ReferenceNumber { get; set; }
    }
}