namespace Checkout.Payments
{
    /// <summary>
    /// Represents a Giropay Alternative Payment source for a payment request.
    /// </summary>
    public class GiropayRequestSource : AlternativePaymentSource
    {
        public const string TypeName = "giropay";

        /// <summary>
        /// Creates a new <see cref="GiropayRequestSource"/> instance with payment method specific request details.
        /// </summary>
        /// <param name="bic">The BIC (8 or 11-digits).</param>
        /// <param name="purpose">The purpose of the payment.</param>
        public GiropayRequestSource(string bic, string purpose)
        {
            Type = TypeName;
            Bic = bic;
            Purpose = purpose;
        }
    }
}
