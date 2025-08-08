namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    PaymentGetResponseSEPAVFourSourceSource
{
    /// <summary>
    /// PaymentGetResponseSEPAV4Source source Class
    /// The source of the payment
    /// </summary>
    public class PaymentGetResponseSEPAVFourSourceSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the PaymentGetResponseSEPAVFourSourceSource class.
        /// </summary>
        public PaymentGetResponseSEPAVFourSourceSource() : base(SourceType.PaymentGetResponseSEPAVFourSource)
        {
        }

        /// <summary>
        /// The instrument ID
        /// [Required]
        /// </summary>
        public string Id { get; set; }
    }
}