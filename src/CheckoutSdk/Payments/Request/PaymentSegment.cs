namespace Checkout.Payments.Request
{
    public class PaymentSegment
    {
        /// <summary>
        /// The brand of the business segment. At least one dimension is required.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The category of the business segment. At least one dimension is required.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string BusinessCategory { get; set; }

        /// <summary>
        /// The market of the business segment. At least one dimension is required.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string Market { get; set; }
    }
}