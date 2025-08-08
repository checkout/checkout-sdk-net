namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Segment
{
    /// <summary>
    /// segment
    /// The dimension details about business segment for payment request. At least one dimension required.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// The brand of business segment.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The category of business segment.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string BusinessCategory { get; set; }

        /// <summary>
        /// The market of business segment.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string Market { get; set; }
    }
}