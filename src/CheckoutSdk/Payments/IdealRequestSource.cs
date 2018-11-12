namespace Checkout.Payments
{
    /// <summary>
    /// Represents an iDeal Alternative Payment source for a payment request.
    /// </summary>
    public class IdealRequestSource : AlternativePaymentSource
    {
        public const string TypeName = "ideal";

        /// <summary>
        /// Creates a new <see cref="IdealRequestSource"/> instance with payment method specific request details.
        /// </summary>
        /// <param name="issuer_id">The issuer_id of the payment.</param>
        public IdealRequestSource(string issuer_id)
        {
            Type = TypeName;
            Issuer_id = issuer_id;
        }
    }
}
