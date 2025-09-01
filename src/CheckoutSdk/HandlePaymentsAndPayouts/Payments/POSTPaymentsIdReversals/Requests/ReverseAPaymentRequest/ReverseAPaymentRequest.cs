namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Requests.ReverseAPaymentRequest
{
    /// <summary>
    /// Reverse a payment
    /// Returns funds back to the customer by automatically performing the appropriate payment action depending on the
    /// payment's status.
    /// For more information, see Reverse a payment.
    /// </summary>
    public class ReverseAPaymentRequest
    {

        /// <summary>
        /// An internal reference to identify the payment reversal.
        /// For American Express payment reversals, there is a 30-character limit.
        /// [Optional]
        /// &lt;= 80
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Stores additional information about the transaction with custom fields.
        /// You can only supply primitive data types with one level of depth. Fields of type object or array are not
        /// supported.
        /// [Optional]
        /// </summary>
        public object Metadata { get; set; }

    }
}
