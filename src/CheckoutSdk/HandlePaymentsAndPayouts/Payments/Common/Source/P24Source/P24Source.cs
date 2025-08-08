namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    P24Source
{
    /// <summary>
    /// p24 source Class
    /// The source of the payment
    /// </summary>
    public class P24Source : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the SepaSource class.
        /// </summary>
        public P24Source() : base(SourceType.P24)
        {
        }

        /// <summary>
        /// P24-generated payment descriptor, which contains the requested billing descriptor or the merchant's default
        /// descriptor (subject to truncation).
        /// [Optional]
        /// </summary>
        public string P24Descriptor { get; set; }
    }
}