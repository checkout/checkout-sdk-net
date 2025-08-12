namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Balances
{
    /// <summary>
    /// balances
    /// The payment balances
    /// </summary>
    public class Balances
    {
        /// <summary>
        /// The total amount that has been authorized
        /// [Optional]
        /// </summary>
        public int TotalAuthorized { get; set; }

        /// <summary>
        /// The total amount that has been voided
        /// [Optional]
        /// </summary>
        public int TotalVoided { get; set; }

        /// <summary>
        /// The total amount that is still available for voiding
        /// [Optional]
        /// </summary>
        public int AvailableToVoid { get; set; }

        /// <summary>
        /// The total amount that has been captured
        /// [Optional]
        /// </summary>
        public int TotalCaptured { get; set; }

        /// <summary>
        /// The total amount that is still available for capture
        /// [Optional]
        /// </summary>
        public int AvailableToCapture { get; set; }

        /// <summary>
        /// The total amount that has been refunded
        /// [Optional]
        /// </summary>
        public int TotalRefunded { get; set; }

        /// <summary>
        /// The total amount that is still available for refund
        /// [Optional]
        /// </summary>
        public int AvailableToRefund { get; set; }
    }
}